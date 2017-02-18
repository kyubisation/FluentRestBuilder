// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.DistributedCacheInputRemoval;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterDistributedCacheInputRemovalPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IDistributedCacheInputRemovalPipeFactory<>),
                typeof(DistributedCacheInputRemovalPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Removes an entry from the cache.
        /// The removed entry (if it exists) is not used in any way.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keyFactory">The key factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> RemoveFromDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, string> keyFactory) =>
            pipe.GetRequiredService<IDistributedCacheInputRemovalPipeFactory<TInput>>()
                .Create(keyFactory, pipe);

        /// <summary>
        /// Removes an entry from the cache.
        /// The removed entry (if it exists) is not used in any way.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> RemoveFromDistributedCache<TInput>(
            this IOutputPipe<TInput> pipe, string key) =>
            pipe.RemoveFromDistributedCache(i => key);
    }
}
