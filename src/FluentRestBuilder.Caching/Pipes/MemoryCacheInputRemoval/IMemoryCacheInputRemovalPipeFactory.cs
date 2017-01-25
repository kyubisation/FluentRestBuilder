// <copyright file="IMemoryCacheInputRemovalPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputRemoval
{
    using System;

    public interface IMemoryCacheInputRemovalPipeFactory<TInput>
    {
        OutputPipe<TInput> Create(Func<TInput, object> keyFactory, IOutputPipe<TInput> parent);
    }
}
