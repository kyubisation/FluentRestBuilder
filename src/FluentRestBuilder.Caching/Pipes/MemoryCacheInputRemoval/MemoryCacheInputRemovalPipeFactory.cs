// <copyright file="MemoryCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputRemoval
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MemoryCacheInputRemovalPipeFactory<TInput> : IMemoryCacheInputRemovalPipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<MemoryCacheInputRemovalPipe<TInput>> logger;

        public MemoryCacheInputRemovalPipeFactory(
            IMemoryCache memoryCache,
            ILogger<MemoryCacheInputRemovalPipe<TInput>> logger = null)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, object> keyFactory, IOutputPipe<TInput> parent) =>
            new MemoryCacheInputRemovalPipe<TInput>(
                keyFactory, this.memoryCache, this.logger, parent);
    }
}