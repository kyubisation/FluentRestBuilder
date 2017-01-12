// <copyright file="MappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;

    public class MappingPipeFactory<TInput, TOutput> :
        IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        public OutputPipe<TOutput> Create(
            Func<TInput, Task<TOutput>> mapping,
            IOutputPipe<TInput> parent) =>
            new MappingPipe<TInput, TOutput>(mapping, parent);
    }
}