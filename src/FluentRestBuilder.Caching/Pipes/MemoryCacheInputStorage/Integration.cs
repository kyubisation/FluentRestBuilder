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
                typeof(IMemoryCacheInputStoragePipeFactory<>), typeof(MemoryCacheInputStoragePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheInputStoragePipeFactory<TInput>>()
                .Create(key, cacheConfigurationCallback, pipe);

        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry> cacheConfigurationCallback)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(key, (e, i) => cacheConfigurationCallback(e));

        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(key, e => e.SetOptions(options));

        public static OutputPipe<TInput> StoreInputInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.StoreInputInMemoryCache(key, (Action<ICacheEntry, TInput>)null);
    }
}
