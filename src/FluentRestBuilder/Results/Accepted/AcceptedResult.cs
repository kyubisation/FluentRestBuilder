// <copyright file="AcceptedResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Accepted
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class AcceptedResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        public AcceptedResult(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        protected override IActionResult CreateResult(TInput source) =>
            new ObjectResult(source) { StatusCode = StatusCodes.Status202Accepted };
    }
}
