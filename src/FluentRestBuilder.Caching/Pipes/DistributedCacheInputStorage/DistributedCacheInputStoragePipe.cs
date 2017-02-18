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
            IOutputPipe<TInput> parent)
            : base(parent)
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
