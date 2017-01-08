// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.Pipes.ActionResultDistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterActionResultDistributedCachePipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IActionResultDistributedCachePipeFactory<>), typeof(ActionResultDistributedCachePipe<>.Factory));
            return builder;
        }

        public static OutputPipe<TInput> UseDistributedCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.GetRequiredService<IActionResultDistributedCachePipeFactory<TInput>>()
                .Resolve(key, options, pipe);

        public static OutputPipe<TInput> UseDistributedCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.UseDistributedCacheForActionResult(key, null);
    }
}
