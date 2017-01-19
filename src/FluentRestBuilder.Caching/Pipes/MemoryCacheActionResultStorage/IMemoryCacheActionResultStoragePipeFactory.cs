// <copyright file="IMemoryCacheActionResultStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheActionResultStorage
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheActionResultStoragePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(
            object key,
            Action<ICacheEntry, TInput> cacheConfigurationCallback,
            IOutputPipe<TInput> pipe);
    }
}
