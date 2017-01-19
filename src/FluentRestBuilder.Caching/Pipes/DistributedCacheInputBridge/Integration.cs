// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Caching.DistributedCache;
    using Caching.Pipes.DistributedCacheInputBridge;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterDistributedCacheInputBridgePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedCacheInputBridgePipeFactory<>),
                typeof(DistributedCacheInputBridgePipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IByteMapper<>), typeof(JsonMapper<>));
            return builder;
        }

        public static OutputPipe<TInput> BridgeIfInputAvailableInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class =>
            pipe.GetRequiredService<IDistributedCacheInputBridgePipeFactory<TInput>>()
                .Create(key, pipe);
    }
}
