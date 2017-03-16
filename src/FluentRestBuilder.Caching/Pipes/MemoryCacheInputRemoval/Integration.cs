// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.MemoryCacheInputRemoval;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterMemoryCacheInputRemovalPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheInputRemovalPipeFactory<>),
                typeof(MemoryCacheInputRemovalPipeFactory<>));
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
        public static OutputPipe<TInput> RemoveFromMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, object> keyFactory)
        {
            var factory = pipe.GetService<IMemoryCacheInputRemovalPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(MemoryCacheInputRemovalPipe<>));
            return factory.Create(keyFactory, pipe);
        }

        /// <summary>
        /// Removes an entry from the cache.
        /// The removed entry (if it exists) is not used in any way.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="key">The key.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> RemoveFromMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key) =>
            pipe.RemoveFromMemoryCache(i => key);
    }
}
