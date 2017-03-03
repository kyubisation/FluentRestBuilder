// <copyright file="OkResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Ok
{
    using Microsoft.Extensions.Logging;

    public class OkResultFactory<TInput> : IOkResultFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<OkResult<TInput>> logger;

        public OkResultFactory(ILogger<OkResult<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new OkResult<TInput>(this.logger, parent);
    }
}