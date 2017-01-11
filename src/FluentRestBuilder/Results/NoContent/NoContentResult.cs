// <copyright file="NoContentResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    using Microsoft.AspNetCore.Mvc;

    public class NoContentResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public NoContentResult(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new NoContentResult();
    }
}
