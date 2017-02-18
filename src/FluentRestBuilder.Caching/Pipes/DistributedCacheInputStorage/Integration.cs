// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.DistributedCache;
    using Caching.Pipes.DistributedCacheInputStorage;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterDistributedCacheInputStoragePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedCacheInputStoragePipeFactory<>),
                typeof(DistributedCacheInputStoragePipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IByteMapper<>), typeof(JsonMapper<>));
            return builder;
        }

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <param name="optionsFactory">The option factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, string> keyFactory,
            Func<TInput, DistributedCacheEntryOptions> optionsFactory)
            where TInput : class =>
            pipe.GetRequiredService<IDistributedCacheInputStoragePipeFactory<TInput>>()
                .Create(keyFactory, optionsFactory, pipe);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <param name="options">The options.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, string> keyFactory,
            DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(keyFactory, i => options);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, string> keyFactory)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(
                keyFactory, (Func<TInput, DistributedCacheEntryOptions>)null);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <param name="optionsFactory">The option factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            string key,
            Func<TInput, DistributedCacheEntryOptions> optionsFactory)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(i => key, optionsFactory);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(key, i => options);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(key, (Func<TInput, DistributedCacheEntryOptions>)null);
    }
}
