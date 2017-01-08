// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.InputMemoryCache;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterInputMemoryCachePipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IInputMemoryCachePipeFactory<>), typeof(InputMemoryCachePipe<>.Factory));
            return builder;
        }

        public static OutputPipe<TInput> UseMemoryCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback)
            where TInput : class =>
            pipe.GetRequiredService<IInputMemoryCachePipeFactory<TInput>>()
                .Resolve(key, cacheConfigurationCallback, pipe);

        public static OutputPipe<TInput> UseMemoryCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            Action<ICacheEntry> cacheConfigurationCallback)
            where TInput : class =>
            pipe.UseMemoryCacheForInput(key, (e, i) => cacheConfigurationCallback(e));

        public static OutputPipe<TInput> UseMemoryCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe,
            object key,
            MemoryCacheEntryOptions options)
            where TInput : class =>
            pipe.UseMemoryCacheForInput(key, e => e.SetOptions(options));

        public static OutputPipe<TInput> UseMemoryCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.UseMemoryCacheForInput(key, (Action<ICacheEntry, TInput>)null);
    }
}
