// <copyright file="IDistributedCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputBridge
{
    public interface IDistributedCacheInputBridgePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(string key, IOutputPipe<TInput> parent);
    }
}
