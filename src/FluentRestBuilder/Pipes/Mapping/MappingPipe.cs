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
        private readonly Func<TInput, TOutput> mapping;

        public MappingPipe(
            Func<TInput, TOutput> mapping,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.mapping = mapping;
        }

        protected override TOutput Map(TInput input) => this.mapping(input);
    }
}
