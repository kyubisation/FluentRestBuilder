// <copyright file="CacheInMemoryCacheOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;

    public static class CacheInMemoryCacheOperator
    {
        /// <summary>
        /// Cache the received value in <see cref="IMemoryCache"/> with the given key
        /// and the defined memory cache options factory function.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="optionsFactory">The factory function for the cache options.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
            this IProviderObservable<TSource> observable,
            object key,
            Func<TSource, MemoryCacheEntryOptions> optionsFactory) =>
            new CacheInMemoryObservable<TSource>(key, optionsFactory, observable);

        private sealed class CacheInMemoryObservable<TSource> : IProviderObservable<TSource>
        {
            private readonly object key;
            private readonly Func<TSource, MemoryCacheEntryOptions> optionsFactory;
            private readonly IProviderObservable<TSource> observable;

            public CacheInMemoryObservable(
                object key,
                Func<TSource, MemoryCacheEntryOptions> optionsFactory,
                IProviderObservable<TSource> observable)
            {
                Check.IsNull(observable, nameof(observable));
                this.key = key;
                this.optionsFactory = optionsFactory;
                this.observable = observable;
                this.ServiceProvider = this.observable.ServiceProvider;
            }

            public IServiceProvider ServiceProvider { get; }

            public IDisposable Subscribe(IObserver<TSource> observer) =>
                this.ResolveCacheValue()
                    .Subscribe(observer);

            private IProviderObservable<TSource> ResolveCacheValue()
            {
                var memoryCache = this.ServiceProvider.GetService<IMemoryCache>();
                return memoryCache.TryGetValue(this.key, out TSource value)
                    ? Observable.Single(value)
                    : this.observable.Do(s =>
                    {
                        var options = this.optionsFactory(s);
                        memoryCache.Set(this.key, s, options);
                    });
            }
        }
    }
}
