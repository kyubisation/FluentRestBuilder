// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.Pipes.InputDistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterInputDistributedCachePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IInputDistributedCachePipeFactory<>),
                typeof(InputDistributedCachePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> UseDistributedCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe, string key, DistributedCacheEntryOptions options)
            where TInput : class =>
            pipe.GetRequiredService<IInputDistributedCachePipeFactory<TInput>>()
                .Resolve(key, options, pipe);

        public static OutputPipe<TInput> UseDistributedCacheForInput<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.UseDistributedCacheForInput(key, null);
    }
}
