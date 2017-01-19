// <copyright file="MemoryCacheInputStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputStorage
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheInputStoragePipeFactory<TInput> : IMemoryCacheInputStoragePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputStoragePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Create(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> pipe) =>
            new MemoryCacheInputStoragePipe<TInput>(
                key, cacheConfigurationCallback, this.memoryCache, pipe);
    }
}