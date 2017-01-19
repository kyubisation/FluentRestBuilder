// <copyright file="IDistributedCacheInputStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputStorage
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedCacheInputStoragePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(
            string key,
            Func<TInput, DistributedCacheEntryOptions> optionGenerator,
            IOutputPipe<TInput> pipe);
    }
}
