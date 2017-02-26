// <copyright file="MappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class MappingPipe<TInput, TOutput> : MappingPipeBase<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly Func<TInput, Task<TOutput>> mapping;

        public MappingPipe(
            Func<TInput, Task<TOutput>> mapping,
            ILogger<MappingPipe<TInput, TOutput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.mapping = mapping;
        }

        protected override async Task<TOutput> MapAsync(TInput input) => await this.mapping(input);
    }
}
