// <copyright file="MemoryCacheActionResultStoragePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultStorage
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheActionResultStoragePipe<TInput> : ChainPipe<TInput>
    {
        private readonly object key;
        private readonly Func<TInput, IActionResult, MemoryCacheEntryOptions> optionFactory;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheActionResultStoragePipe(
            object key,
            Func<TInput, IActionResult, MemoryCacheEntryOptions> optionFactory,
            IMemoryCache memoryCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.optionFactory = optionFactory;
            this.memoryCache = memoryCache;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            var result = await base.Execute(input);
            this.StoreInCache(input, result);
            return result;
        }

        private void StoreInCache(TInput input, IActionResult actionResult)
        {
            var options = this.optionFactory?.Invoke(input, actionResult);
            this.memoryCache.Set(this.key, actionResult, options);
        }
    }
}
