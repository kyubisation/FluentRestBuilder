// <copyright file="CacheInMemoryCacheAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Primitives;

    public static class CacheInMemoryCacheAliases
    {
        /// <summary>
        /// Cache the received value in <see cref="IMemoryCache"/> with the given key
        /// and the defined absolute expiration moment.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="absoluteExpiration">The absolute expiration moment.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
            this IProviderObservable<TSource> observable,
            object key,
            DateTimeOffset absoluteExpiration) =>
            observable.CacheInMemoryCache(
                key, s => new MemoryCacheEntryOptions { AbsoluteExpiration = absoluteExpiration });

        /// <summary>
        /// Cache the received value in <see cref="IMemoryCache"/> with the given key
        /// and the defined absolute expiration moment relative to now.
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
        public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
            this IProviderObservable<TSource> observable,
            object key,
            TimeSpan absoluteExpirationRelativeToNow) =>
            observable.CacheInMemoryCache(
                key,
                s => new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
                });

        /// <summary>
        /// Cache the received value in <see cref="IMemoryCache"/> with the given key
        /// and the given expiration token.
        /// If an entry with the given key is found in the cache, it will be emitted
        /// and the previous chain is skipped.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <param name="expirationToken">The expiration token.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
            this IProviderObservable<TSource> observable,
            object key,
            IChangeToken expirationToken) =>
            observable.CacheInMemoryCache(
                key, s => new MemoryCacheEntryOptions().AddExpirationToken(expirationToken));
    }
}
