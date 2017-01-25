// <copyright file="IDistributedCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.DistributedCacheInputRemoval
{
    using System;

    public interface IDistributedCacheInputRemovalPipeFactory<TInput>
    {
        OutputPipe<TInput> Create(Func<TInput, string> keyFactory, IOutputPipe<TInput> parent);
    }
}
