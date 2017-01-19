// <copyright file="DistributedCacheActionResultStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheActionResultStorage
{
    using System;
    using DistributedCache;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheActionResultStoragePipeFactory<TInput> : IDistributedCacheActionResultStoragePipeFactory<TInput>
    {
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheActionResultStoragePipeFactory(
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache)
        {
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Create(
            string key,
            Func<TInput, IActionResult, DistributedCacheEntryOptions> optionGenerator,
            IOutputPipe<TInput> pipe) =>
            new DistributedCacheActionResultStoragePipe<TInput>(
                key, optionGenerator, this.byteMapper, this.distributedCache, pipe);
    }
}