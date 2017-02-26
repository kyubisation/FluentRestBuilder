// <copyright file="DistributedCacheInputRemovalPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputRemoval
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class DistributedCacheInputRemovalPipe<TInput> : ChainPipe<TInput>
    {
        private readonly Func<TInput, string> keyFactory;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputRemovalPipe(
            Func<TInput, string> keyFactory,
            IDistributedCache distributedCache,
            ILogger<DistributedCacheInputRemovalPipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.keyFactory = keyFactory;
            this.distributedCache = distributedCache;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.RemoveFromCache(input);
            return await base.Execute(input);
        }

        private async Task RemoveFromCache(TInput input)
        {
            var key = this.keyFactory(input);
            await this.distributedCache.RemoveAsync(key);
        }
    }
}
