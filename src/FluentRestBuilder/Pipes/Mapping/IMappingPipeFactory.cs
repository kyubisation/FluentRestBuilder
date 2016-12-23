// <copyright file="IMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;

    public interface IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        OutputPipe<TOutput> Resolve(
            Func<TInput, Task<TOutput>> mapping, IOutputPipe<TInput> parent);
    }
}
