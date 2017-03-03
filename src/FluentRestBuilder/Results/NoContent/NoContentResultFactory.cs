// <copyright file="NoContentResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    using Microsoft.Extensions.Logging;

    public class NoContentResultFactory<TInput> : INoContentResultFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<NoContentResult<TInput>> logger;

        public NoContentResultFactory(ILogger<NoContentResult<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new NoContentResult<TInput>(this.logger, parent);
    }
}