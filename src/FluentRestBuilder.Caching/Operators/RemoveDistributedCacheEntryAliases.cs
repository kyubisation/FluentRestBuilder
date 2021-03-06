﻿// <copyright file="RemoveDistributedCacheEntryAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;

    public static class RemoveDistributedCacheEntryAliases
    {
        /// <summary>
        /// Remove a cache entry from the <see cref="IDistributedCache"/> with the key
        /// generated by the key factory function.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="keyFactory">The key factory function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> RemoveDistributedCacheEntry<TSource>(
            this IProviderObservable<TSource> observable, Func<TSource, string> keyFactory) =>
            observable.DoAsync(async s =>
            {
                var distributedCache = observable.ServiceProvider.GetService<IDistributedCache>();
                var key = keyFactory(s);
                await distributedCache.RemoveAsync(key);
            });

        /// <summary>
        /// Remove a cache entry from the <see cref="IDistributedCache"/> with the key
        /// generated by the key factory function.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> RemoveDistributedCacheEntry<TSource>(
            this IProviderObservable<TSource> observable, string key) =>
            observable.RemoveDistributedCacheEntry(s => key);
    }
}
