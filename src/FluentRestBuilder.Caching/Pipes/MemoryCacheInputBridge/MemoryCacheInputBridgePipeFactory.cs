// <copyright file="MemoryCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputBridge
{
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheInputBridgePipeFactory<TInput> : IMemoryCacheInputBridgePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputBridgePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Create(object key, IOutputPipe<TInput> parent) =>
            new MemoryCacheInputBridgePipe<TInput>(key, this.memoryCache, parent);
    }
}