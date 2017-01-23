// <copyright file="IMemoryCacheInputStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputStorage
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public interface IMemoryCacheInputStoragePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(
            object key,
            Func<TInput, MemoryCacheEntryOptions> optionsFactory,
            IOutputPipe<TInput> pipe);
    }
}
