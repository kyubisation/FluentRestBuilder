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

    public class DistributedCacheInputBridgePipe<TInput> : ChainPipe<TInput>
    {
        private readonly string key;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputBridgePipe(
            string key,
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            IOutputPipe<TInput> parent)
            : base(parent)
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
                return await base.Execute();
            }

            try
            {
                var input = this.byteMapper.FromByteArray(bytes);
                return await this.ExecuteChild(input);
            }
            catch (MappingException)
            {
                return await base.Execute();
            }
        }
    }
}
