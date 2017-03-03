// <copyright file="AcceptedResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Accepted
{
    using Microsoft.Extensions.Logging;

    public class AcceptedResultFactory<TInput> : IAcceptedResultFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<AcceptedResult<TInput>> logger;

        public AcceptedResultFactory(ILogger<AcceptedResult<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public ResultBase<TInput> Create(IOutputPipe<TInput> parent) =>
            new AcceptedResult<TInput>(this.logger, parent);
    }
}