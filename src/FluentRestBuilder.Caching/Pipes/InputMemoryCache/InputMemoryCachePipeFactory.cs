// <copyright file="InputMemoryCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputMemoryCache
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class InputMemoryCachePipeFactory<TInput> : IInputMemoryCachePipeFactory<TInput>
        where TInput : class
    {
        private readonly IMemoryCache memoryCache;

        public InputMemoryCachePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Resolve(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> parent) =>
            new InputMemoryCachePipe<TInput>(
                key, cacheConfigurationCallback, this.memoryCache, parent);
    }
}