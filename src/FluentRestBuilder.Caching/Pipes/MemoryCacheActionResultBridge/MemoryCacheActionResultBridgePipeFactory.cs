// <copyright file="MemoryCacheActionResultBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultBridge
{
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheActionResultBridgePipeFactory<TInput> : IMemoryCacheActionResultBridgePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheActionResultBridgePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Create(object key, IOutputPipe<TInput> parent) =>
            new MemoryCacheActionResultBridgePipe<TInput>(key, this.memoryCache, parent);
    }
}