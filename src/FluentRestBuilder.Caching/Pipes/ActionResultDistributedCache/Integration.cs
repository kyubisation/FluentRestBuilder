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
        public static IFluentRestBuilderCore RegisterActionResultDistributedCachePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IActionResultDistributedCachePipeFactory<>),
                typeof(ActionResultDistributedCachePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> UseDistributedCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.GetRequiredService<IActionResultDistributedCachePipeFactory<TInput>>()
                .Create(key, options, pipe);

        public static OutputPipe<TInput> UseDistributedCacheForActionResult<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.UseDistributedCacheForActionResult(key, null);
    }
}
