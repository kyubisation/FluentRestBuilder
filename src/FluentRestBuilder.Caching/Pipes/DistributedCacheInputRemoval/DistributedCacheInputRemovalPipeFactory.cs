// <copyright file="DistributedCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputRemoval
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class DistributedCacheInputRemovalPipeFactory<TInput> : IDistributedCacheInputRemovalPipeFactory<TInput>
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<DistributedCacheInputRemovalPipe<TInput>> logger;

        public DistributedCacheInputRemovalPipeFactory(
            IDistributedCache distributedCache,
            ILogger<DistributedCacheInputRemovalPipe<TInput>> logger = null)
        {
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, string> keyFactory, IOutputPipe<TInput> parent) =>
            new DistributedCacheInputRemovalPipe<TInput>(
                keyFactory, this.distributedCache, this.logger, parent);
    }
}