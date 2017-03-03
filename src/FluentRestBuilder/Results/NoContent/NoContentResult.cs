// <copyright file="NoContentResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class NoContentResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public NoContentResult(
            ILogger<NoContentResult<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new NoContentResult();
    }
}
