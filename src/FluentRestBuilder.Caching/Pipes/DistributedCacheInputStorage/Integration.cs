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

        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            string key,
            Func<TInput, DistributedCacheEntryOptions> optionGenerator)
            where TInput : class =>
            pipe.GetRequiredService<IDistributedCacheInputStoragePipeFactory<TInput>>()
                .Create(key, optionGenerator, pipe);

        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(key, i => options);

        public static OutputPipe<TInput> StoreInputInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.StoreInputInDistributedCache(key, (Func<TInput, DistributedCacheEntryOptions>)null);
    }
}
