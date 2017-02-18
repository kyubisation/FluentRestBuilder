// <copyright file="DistributedCacheInputStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputStorage
{
    using System;
    using DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheInputStoragePipeFactory<TInput> : IDistributedCacheInputStoragePipeFactory<TInput>
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputStoragePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, string> keyFactory,
            Func<TInput, DistributedCacheEntryOptions> optionGenerator,
            IOutputPipe<TInput> pipe) =>
            new DistributedCacheInputStoragePipe<TInput>(
                keyFactory, optionGenerator, this.byteMapper, this.distributedCache, pipe);
    }
}