// <copyright file="DistributedCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputRemoval
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheInputRemovalPipeFactory<TInput> : IDistributedCacheInputRemovalPipeFactory<TInput>
    {
        private readonly IDistributedCache distributedCache;

        public DistributedCacheInputRemovalPipeFactory(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public OutputPipe<TInput> Create(
            Func<TInput, string> keyFactory, IOutputPipe<TInput> parent) =>
            new DistributedCacheInputRemovalPipe<TInput>(
                keyFactory, this.distributedCache, parent);
    }
}