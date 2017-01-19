// <copyright file="IDistributedCacheActionResultStoragePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheActionResultStorage
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public interface IDistributedCacheActionResultStoragePipeFactory<TInput>
    {
        OutputPipe<TInput> Create(
            string key,
            Func<TInput, IActionResult, DistributedCacheEntryOptions> optionGenerator,
            IOutputPipe<TInput> pipe);
    }
}
