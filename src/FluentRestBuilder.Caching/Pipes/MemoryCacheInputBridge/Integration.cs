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

        /// <summary>
        /// Special pipe that acts as a shortcut, if the cache contains the given key and
        /// the value is of the type TInput.
        /// If that condition is met, the above pipes will be skipped.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The cache key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> BridgeIfInputAvailableInMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key)
            where TInput : class =>
            pipe.GetRequiredService<IMemoryCacheInputBridgePipeFactory<TInput>>()
                .Create(key, pipe);
    }
}
