// <copyright file="OkResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Ok
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class OkResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public OkResult(
            ILogger<OkResult<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new OkObjectResult(source);
    }
}
