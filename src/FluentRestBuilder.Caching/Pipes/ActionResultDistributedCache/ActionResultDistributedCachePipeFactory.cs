// <copyright file="ActionResultDistributedCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultDistributedCache
{
    using DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;

    public class ActionResultDistributedCachePipeFactory<TInput>
        : IActionResultDistributedCachePipeFactory<TInput>
        where TInput : class
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public ActionResultDistributedCachePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Resolve(
            string key, DistributedCacheEntryOptions options, IOutputPipe<TInput> parent) =>
            new ActionResultDistributedCachePipe<TInput>(
                key, options, this.byteMapper, this.distributedCache, parent);
    }
}