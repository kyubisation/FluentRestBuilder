// <copyright file="MemoryCacheInputStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputStorage
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MemoryCacheInputStoragePipeFactory<TInput> : IMemoryCacheInputStoragePipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<MemoryCacheInputStoragePipe<TInput>> logger;

        public MemoryCacheInputStoragePipeFactory(
            IMemoryCache memoryCache,
            ILogger<MemoryCacheInputStoragePipe<TInput>> logger = null)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, object> keyFactory,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory,
            IOutputPipe<TInput> pipe) =>
            new MemoryCacheInputStoragePipe<TInput>(
                keyFactory,
                optionsFactory,
                this.memoryCache,
                this.logger,
                pipe);
    }
}