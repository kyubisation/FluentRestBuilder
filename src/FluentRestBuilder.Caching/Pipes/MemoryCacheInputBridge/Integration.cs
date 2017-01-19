// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.Pipes.MemoryCacheInputBridge;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMemoryCacheInputBridgePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheInputBridgePipeFactory<>),
                typeof(MemoryCacheInputBridgePipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> BridgeIfInputAvailableInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheInputBridgePipeFactory<TInput>>()
                .Create(key, pipe);
    }
}
