﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.Pipes.DistributedInputCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterDistributedInputCachePipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedInputCachePipeFactory<>), typeof(DistributedInputCachePipe<>.Factory));
            return builder;
        }

        public static OutputPipe<TInput> UseDistributedCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.GetRequiredService<IDistributedInputCachePipeFactory<TInput>>()
                .Resolve(key, options, pipe);

        public static OutputPipe<TInput> UseDistributedCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.UseDistributedCacheForInput(key, null);
    }
}
