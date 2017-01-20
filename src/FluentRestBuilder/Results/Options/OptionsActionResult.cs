// <copyright file="OptionsActionResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    public class OptionsActionResult : OkResult
    {
        private readonly string allowedOptions;

        public OptionsActionResult(string allowedOptions)
        {
            this.allowedOptions = allowedOptions;
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.Headers.Append(
                "Allow", new StringValues(this.allowedOptions));
        }
    }
}
