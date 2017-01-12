// <copyright file="InputDistributedCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputDistributedCache
{
    using DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;

    public class InputDistributedCachePipeFactory<TInput> : IInputDistributedCachePipeFactory<TInput>
        where TInput : class
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public InputDistributedCachePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Create(
            string key, DistributedCacheEntryOptions options, IOutputPipe<TInput> parent) =>
            new InputDistributedCachePipe<TInput>(
                key, options, this.byteMapper, this.distributedCache, parent);
    }
}