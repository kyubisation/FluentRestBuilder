// <copyright file="AcceptedResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Results.AcceptedResult
{
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AcceptedResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        public AcceptedResultPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new ObjectResult(source) { StatusCode = StatusCodes.Status202Accepted };
    }
}
