// <copyright file="IInputMemoryCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.InputMemoryCache
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public interface IInputMemoryCachePipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> parent);
    }
}
