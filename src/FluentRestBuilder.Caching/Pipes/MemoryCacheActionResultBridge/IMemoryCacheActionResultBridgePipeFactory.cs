// <copyright file="IMemoryCacheActionResultBridgePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultBridge
{
    public interface IMemoryCacheActionResultBridgePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(object key, IOutputPipe<TInput> parent);
    }
}
