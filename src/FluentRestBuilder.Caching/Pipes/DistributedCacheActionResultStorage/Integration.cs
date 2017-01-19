// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.DistributedCache;
    using Caching.Pipes.DistributedCacheActionResultStorage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterDistributedCacheActionResultStoragePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedCacheActionResultStoragePipeFactory<>),
                typeof(DistributedCacheActionResultStoragePipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IByteMapper<>), typeof(JsonMapper<>));
            return builder;
        }

        public static OutputPipe<TInput> StoreActionResultInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            string key,
            Func<TInput, IActionResult, DistributedCacheEntryOptions> optionGenerator)
            where TInput : class =>
            pipe.GetRequiredService<IDistributedCacheActionResultStoragePipeFactory<TInput>>()
                .Create(key, optionGenerator, pipe);

        public static OutputPipe<TInput> StoreActionResultInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe,
            string key,
            Func<TInput, DistributedCacheEntryOptions> optionGenerator)
            where TInput : class =>
            pipe.StoreActionResultInDistributedCache(key, (i, a) => optionGenerator(i));

        public static OutputPipe<TInput> StoreActionResultInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreActionResultInDistributedCache(key, (i, a) => options);

        public static OutputPipe<TInput> StoreActionResultInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.StoreActionResultInDistributedCache(
                key, (Func<TInput, IActionResult, DistributedCacheEntryOptions>)null);
    }
}
