// <copyright file="MemoryCacheActionResultBridgePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultBridge
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheActionResultBridgePipe<TInput> : ChainPipe<TInput>
    {
        private readonly object key;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheActionResultBridgePipe(
            object key,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.memoryCache = memoryCache;
        }

        protected override Task<IActionResult> Execute()
        {
            object cacheEntry;
            if (!this.memoryCache.TryGetValue(this.key, out cacheEntry))
            {
                return base.Execute();
            }

            var actionResult = cacheEntry as IActionResult;
            return actionResult != null ? Task.FromResult(actionResult) : base.Execute();
        }
    }
}
