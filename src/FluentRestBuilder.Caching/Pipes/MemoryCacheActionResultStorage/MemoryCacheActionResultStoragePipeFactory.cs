// <copyright file="MemoryCacheActionResultStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultStorage
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheActionResultStoragePipeFactory<TInput> : IMemoryCacheActionResultStoragePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheActionResultStoragePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Create(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> pipe) =>
            new MemoryCacheInputStorage.MemoryCacheInputStoragePipe<TInput>(
                key, cacheConfigurationCallback, this.memoryCache, pipe);
    }
}