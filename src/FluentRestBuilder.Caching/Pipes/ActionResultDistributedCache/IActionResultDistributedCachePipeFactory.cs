// <copyright file="IActionResultDistributedCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultDistributedCache
{
    using Microsoft.Extensions.Caching.Distributed;

    public interface IActionResultDistributedCachePipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(
            string key, DistributedCacheEntryOptions options, IOutputPipe<TInput> parent);
    }
}
