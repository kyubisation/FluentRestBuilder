// <copyright file="DistributedCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputBridge
{
    using DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheInputBridgePipeFactory<TInput> : IDistributedCacheInputBridgePipeFactory<TInput>
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputBridgePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Create(string key, IOutputPipe<TInput> parent) =>
            new DistributedCacheInputBridgePipe<TInput>(
                key, this.byteMapper, this.distributedCache, parent);
    }
}