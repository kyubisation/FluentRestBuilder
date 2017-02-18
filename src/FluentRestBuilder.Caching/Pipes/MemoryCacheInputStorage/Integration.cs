// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.MemoryCacheInputStorage;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMemoryCacheInputStoragePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheInputStoragePipeFactory<>),
                typeof(MemoryCacheInputStoragePipeFactory<>));
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
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<TInput, object> keyFactory,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheInputStoragePipeFactory<TInput>>()
                .Create(keyFactory, optionsFactory, pipe);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <param name="options">The options.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, object> keyFactory, MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(keyFactory, e => options);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, object> keyFactory)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(keyFactory, (MemoryCacheEntryOptions)null);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <param name="optionsFactory">The option factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(i => key, optionsFactory);

        /// <summary>
        /// Store the received input in cache with the given key and options.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <param name="options">The options.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key, MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(key, e => options);

        /// <summary>
        /// Store the received input in cache with the given key.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(key, (MemoryCacheEntryOptions)null);
    }
}
