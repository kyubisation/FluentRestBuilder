// <copyright file="InputMemoryCachePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputMemoryCache
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class InputMemoryCachePipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly object key;
        private readonly Action<ICacheEntry, TInput> cacheConfigurationCallback;
        private readonly IMemoryCache memoryCache;

        public InputMemoryCachePipe(
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

        protected override IActionResult Execute(TInput entity)
        {
            if (entity != null)
            {
                this.SaveToCache(entity);
            }

            return base.Execute(entity);
        }

        protected override async Task<IActionResult> Execute()
        {
            var cacheEntry = this.RetrieveFromCache();
            if (cacheEntry != null)
            {
                return await this.ExecuteChild(cacheEntry);
            }

            return await base.Execute();
        }

        private void SaveToCache(TInput input)
        {
            var cacheEntry = this.memoryCache.CreateEntry(this.key);
            cacheEntry.Value = input;
            this.cacheConfigurationCallback?.Invoke(cacheEntry, input);
        }

        private TInput RetrieveFromCache()
        {
            object cacheObject;
            if (this.memoryCache.TryGetValue(this.key, out cacheObject))
            {
                return cacheObject as TInput;
            }

            return null;
        }

        public class Factory : IInputMemoryCachePipeFactory<TInput>
        {
            private readonly IMemoryCache memoryCache;

            public Factory(IMemoryCache memoryCache)
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
}
