// <copyright file="CacheInDistributedCacheOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Caching.Operators.DistributedCache;
    using Disposables;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;

    public static class CacheInDistributedCacheOperator
    {
        /// <summary>
        /// Cache the received value in <see cref="IDistributedCache"/> with the given key
        /// and the defined distributed cache options factory function.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="optionsFactory">The factory function for the cache options.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
            this IProviderObservable<TSource> observable,
            string key,
            Func<TSource, DistributedCacheEntryOptions> optionsFactory) =>
            new CacheInDistributedCacheObservable<TSource>(key, optionsFactory, observable);

        private sealed class CacheInDistributedCacheObservable<TSource> : IProviderObservable<TSource>
        {
            private readonly string key;
            private readonly Func<TSource, DistributedCacheEntryOptions> optionsFactory;
            private readonly IProviderObservable<TSource> observable;

            public CacheInDistributedCacheObservable(
                string key,
                Func<TSource, DistributedCacheEntryOptions> optionsFactory,
                IProviderObservable<TSource> observable)
            {
                Check.IsNull(observable, nameof(observable));
                this.key = key;
                this.optionsFactory = optionsFactory;
                this.observable = observable;
                this.ServiceProvider = this.observable.ServiceProvider;
            }

            public IServiceProvider ServiceProvider { get; }

            public IDisposable Subscribe(IObserver<TSource> observer)
            {
                var tokenSource = new CancellationTokenSource();
                var task = Task.Run(
                    () => this.ResolveCacheValue(tokenSource.Token), tokenSource.Token);
                task.ContinueWith(
                    t => observer.OnError(new OperationCanceledException()),
                    TaskContinuationOptions.OnlyOnCanceled);
                task.ContinueWith(
                    t => observer.OnError(t.Exception.InnerException),
                    TaskContinuationOptions.OnlyOnFaulted);
                var disposables = new DisposableCollection(tokenSource);
                task.ContinueWith(
                    t => disposables.Add(t.Result.Subscribe(observer)),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
                return disposables;
            }

            private async Task<IProviderObservable<TSource>> ResolveCacheValue(CancellationToken token)
            {
                var distributedCache = this.ServiceProvider.GetService<IDistributedCache>();
                var bytes = await distributedCache.GetAsync(this.key, token);
                if (bytes == null || bytes.Length != 0)
                {
                    return this.observable
                        .DoAsync(async s => await this.StoreInCache(s, distributedCache, token));
                }

                var byteMapper = this.ResolveByteMapper();
                var value = byteMapper.FromByteArray(bytes);
                return Observable.Single(value);
            }

            private async Task StoreInCache(
                TSource value, IDistributedCache distributedCache, CancellationToken token)
            {
                var options = this.optionsFactory?.Invoke(value)
                    ?? new DistributedCacheEntryOptions();
                var byteArray = this.ConvertToByteArray(value);
                await distributedCache.SetAsync(this.key, byteArray, options, token);
            }

            private byte[] ConvertToByteArray(TSource value)
            {
                var byteMapper = this.ResolveByteMapper();
                return byteMapper.ToByteArray(value);
            }

            private IByteMapper<TSource> ResolveByteMapper() =>
                this.ServiceProvider.GetService<IByteMapper<TSource>>()
                ?? new JsonMapper<TSource>();
        }
    }
}
