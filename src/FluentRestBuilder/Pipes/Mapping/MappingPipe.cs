// <copyright file="MappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;

    public class MappingPipe<TInput, TOutput> : MappingPipeBase<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TInput, Task<TOutput>> mapping;

        public MappingPipe(
            Func<TInput, Task<TOutput>> mapping,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.mapping = mapping;
        }

        protected override async Task<TOutput> MapAsync(TInput input) => await this.mapping(input);
    }
}
