// <copyright file="MappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;

    public class MappingPipe<TInput, TOutput> : BaseMappingPipe<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        public MappingPipe(
            Func<TInput, TOutput> transformation,
            IOutputPipe<TInput> parent)
            : base(transformation, parent)
        {
        }
    }
}
