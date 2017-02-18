// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;

    public static partial class Integration
    {
        /// <summary>
        /// Registers the caching pipes.
        /// </summary>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The given FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddCachingPipes(
            this IFluentRestBuilder builder)
        {
            new FluentRestBuilderCore(builder.Services)
                .RegisterDistributedCacheInputStoragePipe()
                .RegisterDistributedCacheInputRemovalPipe()
                .RegisterDistributedCacheInputBridgePipe()
                .RegisterMemoryCacheInputStoragePipe()
                .RegisterMemoryCacheInputRemovalPipe()
                .RegisterMemoryCacheInputBridgePipe();
            return builder;
        }
    }
}
