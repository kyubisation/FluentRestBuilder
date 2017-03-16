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
    using Microsoft.Extensions.Logging;

    public class OptionsResult<TInput> : ResultBase<TInput>
        where TInput : class
    {
        private readonly Func<TInput, IEnumerable<HttpVerb>> verbGeneration;
        private readonly IHttpVerbDictionary httpVerbDictionary;

        public OptionsResult(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IHttpVerbDictionary httpVerbDictionary,
            ILogger<OptionsResult<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.verbGeneration = verbGeneration;
            this.httpVerbDictionary = httpVerbDictionary;
        }

        protected override IActionResult CreateResult(TInput source)
        {
            var allowedOptions = this.BuildAllowedOptions(source);
            return new OptionsActionResult(allowedOptions);
        }

        private string BuildAllowedOptions(TInput input)
        {
            return this.verbGeneration(input)
                .Select(v => this.httpVerbDictionary[v])
                .Aggregate(
                    new StringBuilder("OPTIONS"),
                    (current, next) => current.Append($", {next}"))
                .ToString();
        }
    }
}
