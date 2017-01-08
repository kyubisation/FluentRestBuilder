// <copyright file="IInputDistributedCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputDistributedCache
{
    using Microsoft.Extensions.Caching.Distributed;

    public interface IInputDistributedCachePipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            string key, DistributedCacheEntryOptions options, IOutputPipe<TInput> parent);
    }
}
