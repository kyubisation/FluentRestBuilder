// <copyright file="OkResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Results.OkResult
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class OkResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        public OkResultPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new OkObjectResult(source);
    }
}
