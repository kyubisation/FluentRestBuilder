// <copyright file="NoContentResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.NoContent
{
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class NoContentResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        public NoContentResultPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new NoContentResult();
    }
}
