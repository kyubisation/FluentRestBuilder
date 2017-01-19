// <copyright file="IMemoryCacheInputBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputBridge
{
    public interface IMemoryCacheInputBridgePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(object key, IOutputPipe<TInput> parent);
    }
}
