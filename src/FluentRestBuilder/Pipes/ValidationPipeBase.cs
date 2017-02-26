// <copyright file="ValidationPipeBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class ValidationPipeBase<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly Func<TInput, object> errorFactory;
        private readonly int statusCode;

        protected ValidationPipeBase(
            int statusCode,
            Func<TInput, object> errorFactory,
            ILogger logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.statusCode = statusCode;
            this.errorFactory = errorFactory;
        }

        protected override async Task<IActionResult> GenerateActionResult(TInput input) =>
            await this.IsInvalid(input) ? this.CreateErrorResult(input) : null;

        protected abstract Task<bool> IsInvalid(TInput input);

        private IActionResult CreateErrorResult(TInput input)
        {
            var error = this.errorFactory?.Invoke(input);
            return error == null
                ? this.CreateErrorActionResult() : this.CreateErrorObjectResult(error);
        }

        private IActionResult CreateErrorActionResult()
        {
            switch (this.statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestResult();
                case StatusCodes.Status404NotFound:
                    return new NotFoundResult();
                default:
                    return new StatusCodeResult(this.statusCode);
            }
        }

        private IActionResult CreateErrorObjectResult(object error)
        {
            switch (this.statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestObjectResult(error);
                case StatusCodes.Status404NotFound:
                    return new NotFoundObjectResult(error);
                default:
                    return new ObjectResult(error) { StatusCode = this.statusCode };
            }
        }
    }
}
