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
        private readonly Func<TInput, MemoryCacheEntryOptions> optionsFactory;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputStoragePipe(
            object key,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.optionsFactory = optionsFactory;
            this.memoryCache = memoryCache;
        }

        protected override Task<IActionResult> Execute(TInput input)
        {
            this.StoreInCache(input);
            return base.Execute(input);
        }

        private void StoreInCache(TInput input)
        {
            var options = this.optionsFactory?.Invoke(input);
            this.memoryCache.Set(this.key, input, options);
        }
    }
}
