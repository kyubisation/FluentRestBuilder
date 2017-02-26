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

        /// <summary>
        /// Special pipe that acts as a shortcut, if the cache contains the given key and
        /// the value is of the type TInput.
        /// If that condition is met, the above pipes will be skipped.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BridgeIfInputAvailableInDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key)
            where TInput : class
        {
            var factory = pipe.GetService<IDistributedCacheInputBridgePipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(DistributedCacheInputBridgePipe<>));
            return factory.Create(key, pipe);
        }
    }
}
