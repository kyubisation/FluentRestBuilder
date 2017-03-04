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
    using Microsoft.Extensions.Logging;

    public class MemoryCacheInputStoragePipe<TInput> : ChainPipe<TInput>
    {
        private readonly Func<TInput, object> keyFactory;
        private readonly Func<TInput, MemoryCacheEntryOptions> optionsFactory;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputStoragePipe(
            Func<TInput, object> keyFactory,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory,
            IMemoryCache memoryCache,
            ILogger<MemoryCacheInputStoragePipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.keyFactory = keyFactory;
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
            var key = this.keyFactory(input);
            this.Logger.Information?.Log(
                "Caching input value of type {0} with key {1}", typeof(TInput), key);
            this.Logger.Trace?.Log("Cache value {0}", input);
            this.memoryCache.Set(key, input, options);
        }
    }
}
