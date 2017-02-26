// <copyright file="MappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class MappingPipeFactory<TInput, TOutput> :
        IMappingPipeFactory<TInput, TOutput>
        where TInput : class
        where TOutput : class
    {
        private readonly ILogger<MappingPipe<TInput, TOutput>> logger;

        public MappingPipeFactory(ILogger<MappingPipe<TInput, TOutput>> logger = null)
        {
            this.logger = logger;
        }

        public OutputPipe<TOutput> Create(
            Func<TInput, Task<TOutput>> mapping,
            IOutputPipe<TInput> parent) =>
            new MappingPipe<TInput, TOutput>(mapping, this.logger, parent);
    }
}