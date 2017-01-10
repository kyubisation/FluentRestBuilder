// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.ActionResultMemoryCache;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterActionResultMemoryCachePipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IActionResultMemoryCachePipeFactory<>),
                typeof(ActionResultMemoryCachePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> UseMemoryCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry, IActionResult> cacheConfigurationCallback)
            where TInput : class =>
            pipe.GetRequiredService<IActionResultMemoryCachePipeFactory<TInput>>()
                .Resolve(key, cacheConfigurationCallback, pipe);

        public static OutputPipe<TInput> UseMemoryCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry> cacheConfigurationCallback)
            where TInput : class =>
            pipe.UseMemoryCacheForActionResult(key, (e, a) => cacheConfigurationCallback(e));

        public static OutputPipe<TInput> UseMemoryCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.UseMemoryCacheForActionResult(key, e => e.SetOptions(options));

        public static OutputPipe<TInput> UseMemoryCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.UseMemoryCacheForActionResult(key, (Action<ICacheEntry, IActionResult>)null);
    }
}
