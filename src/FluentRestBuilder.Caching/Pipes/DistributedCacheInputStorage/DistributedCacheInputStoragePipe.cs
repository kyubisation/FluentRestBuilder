// <copyright file="DistributedCacheInputStoragePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputStorage
{
    using System;
    using System.Threading.Tasks;
    using DistributedCache;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class DistributedCacheInputStoragePipe<TInput> : ChainPipe<TInput>
    {
        private readonly Func<TInput, string> keyFactory;
        private readonly Func<TInput, DistributedCacheEntryOptions> optionFactory;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputStoragePipe(
            Func<TInput, string> keyFactory,
            Func<TInput, DistributedCacheEntryOptions> optionFactory,
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            ILogger<DistributedCacheInputStoragePipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.keyFactory = keyFactory;
            this.optionFactory = optionFactory;
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.StoreInCache(input);
            return await base.Execute(input);
        }

        private async Task StoreInCache(TInput input)
        {
            var cacheBytes = this.byteMapper.ToByteArray(input);
            var options = this.optionFactory?.Invoke(input);
            var key = this.keyFactory(input);
            this.Logger.Information?.Log(
                "Caching input value of type {0} with key {1}", typeof(TInput), key);
            this.Logger.Trace?.Log("Cache value {0}", input);
            if (options == null)
            {
                await this.distributedCache.SetAsync(key, cacheBytes);
            }
            else
            {
                await this.distributedCache.SetAsync(key, cacheBytes, options);
            }
        }
    }
}
