// <copyright file="MemoryCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputRemoval
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheInputRemovalPipeFactory<TInput> : IMemoryCacheInputRemovalPipeFactory<TInput>
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputRemovalPipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, object> keyFactory, IOutputPipe<TInput> parent) =>
            new MemoryCacheInputRemovalPipe<TInput>(keyFactory, this.memoryCache, parent);
    }
}