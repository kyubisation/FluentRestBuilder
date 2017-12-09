// <copyright file="CacheInDistributedCacheAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;

    public static class CacheInDistributedCacheAliases
    {
        /// <summary>
        /// Cache the received value in <see cref="IDistributedCache"/> with the given key.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
            this IProviderObservable<TSource> observable, string key) =>
            observable.CacheInDistributedCache(key, s => new DistributedCacheEntryOptions());

        /// <summary>
        /// Cache the received value in <see cref="IDistributedCache"/> with the given key
        /// and the defined absolute expiration moment.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="absoluteExpiration">The absolute expiration moment.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
            this IProviderObservable<TSource> observable,
            string key,
            DateTimeOffset absoluteExpiration) =>
            observable.CacheInDistributedCache(
                key,
                s => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = absoluteExpiration,
                });

        /// <summary>
        /// Cache the received value in <see cref="IDistributedCache"/> with the given key
        /// and the defined absolute expiration moment.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="absoluteExpirationRelativeToNow">
        /// The absolute expiration moment relative to now.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
            this IProviderObservable<TSource> observable,
            string key,
            TimeSpan absoluteExpirationRelativeToNow) =>
            observable.CacheInDistributedCache(
                key,
                s => new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
                });
    }
}
