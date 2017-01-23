// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.MemoryCacheActionResultStorage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMemoryCacheActionResultStoragePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheActionResultStoragePipeFactory<>),
                typeof(MemoryCacheActionResultStoragePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> StoreActionResultInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Func<TInput, IActionResult, MemoryCacheEntryOptions> optionFactory)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheActionResultStoragePipeFactory<TInput>>()
                .Create(key, optionFactory, pipe);

        public static OutputPipe<TInput> StoreActionResultInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Func<IActionResult, MemoryCacheEntryOptions> optionFactory)
            where TInput : class =>
            pipe.StoreActionResultInMemoryCache(key, (i, r) => optionFactory(r));

        public static OutputPipe<TInput> StoreActionResultInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.StoreActionResultInMemoryCache(key, (i, r) => options);

        public static OutputPipe<TInput> StoreActionResultInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.StoreActionResultInMemoryCache(key, (MemoryCacheEntryOptions)null);
    }
}
