// <copyright file="MemoryCacheInputStoragePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputStorage
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheInputStoragePipe<TInput> : ChainPipe<TInput>
    {
        private readonly object key;
        private readonly Action<ICacheEntry, TInput> cacheConfigurationCallback;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputStoragePipe(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.cacheConfigurationCallback = cacheConfigurationCallback;
            this.memoryCache = memoryCache;
        }

        protected override Task<IActionResult> Execute(TInput input)
        {
            this.StoreInCache(input);
            return base.Execute(input);
        }

        private void StoreInCache(TInput input)
        {
            var cacheEntry = this.memoryCache.CreateEntry(this.key);
            cacheEntry.Value = input;
            this.cacheConfigurationCallback?.Invoke(cacheEntry, input);
        }
    }
}
