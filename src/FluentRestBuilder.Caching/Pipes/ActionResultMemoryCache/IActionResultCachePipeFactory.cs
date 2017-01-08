// <copyright file="IActionResultCachePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultMemoryCache
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public interface IActionResultCachePipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            object key,
            Action<ICacheEntry, IActionResult> cacheConfigurationCallback,
            IOutputPipe<TInput> parent);
    }
}
