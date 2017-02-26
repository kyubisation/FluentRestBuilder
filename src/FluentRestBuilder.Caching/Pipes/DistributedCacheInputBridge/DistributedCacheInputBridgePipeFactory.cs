// <copyright file="DistributedCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputBridge
{
    using DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class DistributedCacheInputBridgePipeFactory<TInput> : IDistributedCacheInputBridgePipeFactory<TInput>
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<DistributedCacheInputBridgePipe<TInput>> logger;

        public DistributedCacheInputBridgePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            ILogger<DistributedCacheInputBridgePipe<TInput>> logger = null)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(string key, IOutputPipe<TInput> parent) =>
            new DistributedCacheInputBridgePipe<TInput>(
                key, this.byteMapper, this.distributedCache, this.logger, parent);
    }
}