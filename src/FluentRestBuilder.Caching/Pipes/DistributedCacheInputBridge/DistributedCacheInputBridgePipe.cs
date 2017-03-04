// <copyright file="DistributedCacheInputBridgePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputBridge
{
    using System.Threading.Tasks;
    using DistributedCache;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;

    public class DistributedCacheInputBridgePipe<TInput> : ChainPipe<TInput>
    {
        private readonly string key;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputBridgePipe(
            string key,
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            ILogger<DistributedCacheInputBridgePipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.key = key;
            this.byteMapper = byteMapper;
            this.distributedCache = distributedCache;
        }

        protected override async Task<IActionResult> Execute()
        {
            var bytes = await this.distributedCache.GetAsync(this.key);
            if (bytes == null || bytes.Length == 0)
            {
                this.Logger.Information?.Log("No cache entry found with key {0}", this.key);
                return await base.Execute();
            }

            try
            {
                var input = this.byteMapper.FromByteArray(bytes);
                this.Logger.Information?.Log("Found cache entry with key {0}", this.key);
                this.Logger.Trace?.Log("Cache entry: {0}", input);
                return await this.ExecuteChild(input);
            }
            catch (MappingException)
            {
                this.Logger.Warning?.Log(
                    "Cache entry with key {0} could not be mapped to {1}",
                    this.key,
                    typeof(TInput));
                return await base.Execute();
            }
        }
    }
}
