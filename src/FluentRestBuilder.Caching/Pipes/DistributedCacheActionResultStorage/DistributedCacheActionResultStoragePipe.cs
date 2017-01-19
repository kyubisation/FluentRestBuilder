// <copyright file="DistributedCacheActionResultStoragePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheActionResultStorage
{
    using System;
    using System.Threading.Tasks;
    using DistributedCache;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheActionResultStoragePipe<TInput> : ChainPipe<TInput>
    {
        private readonly string key;
        private readonly Func<TInput, IActionResult, DistributedCacheEntryOptions> optionGenerator;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheActionResultStoragePipe(
            string key,
            Func<TInput, IActionResult, DistributedCacheEntryOptions> optionGenerator,
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
            var result = await base.Execute(input);
            await this.StoreInCache(input, result);
            return result;
        }

        private async Task StoreInCache(TInput input, IActionResult actionResult)
        {
            var cacheBytes = this.byteMapper.ToByteArray(input);
            var options = this.optionGenerator?.Invoke(input, actionResult);
            await this.distributedCache.SetAsync(this.key, cacheBytes, options);
        }
    }
}
