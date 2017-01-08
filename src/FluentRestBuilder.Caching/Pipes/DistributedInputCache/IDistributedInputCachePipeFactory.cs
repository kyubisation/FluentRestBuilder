// <copyright file="IDistributedInputCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedInputCache
{
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedInputCachePipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            string key, DistributedCacheEntryOptions options, IOutputPipe<TInput> parent);
    }
}
