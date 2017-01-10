// <copyright file="ActionResultMemoryCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultMemoryCache
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class ActionResultMemoryCachePipeFactory<TInput> : IActionResultMemoryCachePipeFactory<TInput>
        where TInput : class
    {
        private readonly IMemoryCache memoryCache;

        public ActionResultMemoryCachePipeFactory(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public OutputPipe<TInput> Resolve(
            object key,
            Action<ICacheEntry, IActionResult> cacheConfigurationCallback,
            IOutputPipe<TInput> parent) =>
            new ActionResultMemoryCachePipe<TInput>(
                key, cacheConfigurationCallback, this.memoryCache, parent);
    }
}