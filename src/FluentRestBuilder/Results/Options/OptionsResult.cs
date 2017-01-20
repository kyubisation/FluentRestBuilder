// <copyright file="OptionsResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Mvc;

    public class OptionsResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        private readonly Func<TInput, IEnumerable<HttpVerb>> verbGeneration;
        private readonly IHttpVerbMap httpVerbMap;

        public OptionsResult(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IHttpVerbMap httpVerbMap,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.verbGeneration = verbGeneration;
            this.httpVerbMap = httpVerbMap;
        }

        protected override IActionResult CreateResult(TInput source)
        {
            var allowedOptions = this.BuildAllowedOptions(source);
            return new OptionsActionResult(allowedOptions);
        }

        private string BuildAllowedOptions(TInput input)
        {
            return this.verbGeneration(input)
                .Select(v => this.httpVerbMap[v])
                .Aggregate(
                    new StringBuilder("OPTIONS"),
                    (current, next) => current.Append($", {next}"))
                .ToString();
        }
    }
}
