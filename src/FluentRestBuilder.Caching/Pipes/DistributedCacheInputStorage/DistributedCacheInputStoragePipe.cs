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
        private readonly string key;
        private readonly Func<TInput, DistributedCacheEntryOptions> optionGenerator;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputStoragePipe(
            string key,
            Func<TInput, DistributedCacheEntryOptions> optionGenerator,
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.optionGenerator = optionGenerator;
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
            var options = this.optionGenerator?.Invoke(input);
            if (options == null)
            {
                await this.distributedCache.SetAsync(this.key, cacheBytes);
            }
            else
            {
                await this.distributedCache.SetAsync(this.key, cacheBytes, options);
            }
        }
    }
}
