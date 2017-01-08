// <copyright file="IInputMemoryCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputMemoryCache
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public interface IInputMemoryCachePipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> parent);
    }
}
