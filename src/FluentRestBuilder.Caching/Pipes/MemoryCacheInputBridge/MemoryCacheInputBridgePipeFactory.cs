// <copyright file="MemoryCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputBridge
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MemoryCacheInputBridgePipeFactory<TInput> : IMemoryCacheInputBridgePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<MemoryCacheInputBridgePipe<TInput>> logger;

        public MemoryCacheInputBridgePipeFactory(
            IMemoryCache memoryCache,
            ILogger<MemoryCacheInputBridgePipe<TInput>> logger = null)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(object key, IOutputPipe<TInput> parent) =>
            new MemoryCacheInputBridgePipe<TInput>(key, this.memoryCache, this.logger, parent);
    }
}