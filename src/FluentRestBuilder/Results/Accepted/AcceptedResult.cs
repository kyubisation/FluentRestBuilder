// <copyright file="AcceptedResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Accepted
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class AcceptedResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public AcceptedResult(
            ILogger<AcceptedResult<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new ObjectResult(source) { StatusCode = StatusCodes.Status202Accepted };
    }
}
