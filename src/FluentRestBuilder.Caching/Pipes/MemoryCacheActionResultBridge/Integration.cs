// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.Pipes.MemoryCacheActionResultBridge;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMemoryCacheActionResultBridgePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheActionResultBridgePipeFactory<>),
                typeof(MemoryCacheActionResultBridgePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> BridgeIfActionResultAvailableInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheActionResultBridgePipeFactory<TInput>>()
                .Create(key, pipe);
    }
}
