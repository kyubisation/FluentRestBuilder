// <copyright file="MappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;

    public class MappingPipeFactory<TInput, TOutput> :
        IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        public OutputPipe<TOutput> Resolve(
            Func<TInput, TOutput> transformation,
            IOutputPipe<TInput> parent) =>
            new MappingPipe<TInput, TOutput>(transformation, parent);
    }
}