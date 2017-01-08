// <copyright file="ActionResultMemoryCachePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultMemoryCache
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class ActionResultMemoryCachePipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly object key;
        private readonly Action<ICacheEntry, IActionResult> cacheConfigurationCallback;
        private readonly IMemoryCache memoryCache;

        public ActionResultMemoryCachePipe(
            object key,
            Action<ICacheEntry, IActionResult> cacheConfigurationCallback,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.cacheConfigurationCallback = cacheConfigurationCallback;
            this.memoryCache = memoryCache;
        }

        protected override async Task<IActionResult> GenerateActionResultAsync(TInput entity)
        {
            var actionResult = await this.ExecuteChild(entity);
            this.SaveToCache(actionResult);
            return actionResult;
        }

        protected override async Task<IActionResult> Execute()
        {
            var cacheEntry = this.RetrieveFromCache();
            if (cacheEntry != null)
            {
                return cacheEntry;
            }

            return await base.Execute();
        }

        private void SaveToCache(IActionResult actionResult)
        {
            var cacheEntry = this.memoryCache.CreateEntry(this.key);
            cacheEntry.Value = actionResult;
            this.cacheConfigurationCallback?.Invoke(cacheEntry, actionResult);
        }

        private IActionResult RetrieveFromCache()
        {
            object cacheObject;
            if (this.memoryCache.TryGetValue(this.key, out cacheObject))
            {
                return cacheObject as IActionResult;
            }

            return null;
        }

        public class Factory : IActionResultCachePipeFactory<TInput>
        {
            private readonly IMemoryCache memoryCache;

            public Factory(IMemoryCache memoryCache)
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
}
