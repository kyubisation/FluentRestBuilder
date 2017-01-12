// <copyright file="OkResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Ok
{
    using Microsoft.AspNetCore.Mvc;

    public class OkResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public OkResult(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new OkObjectResult(source);
    }
}
