// <copyright file="OptionsResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using Storage;

    public class OptionsResultPipe<TInput> : ResultBase<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<HttpContext> httpContextStorage;
        private readonly Func<TInput, IEnumerable<HttpVerb>> verbGeneration;
        private readonly IHttpVerbMap httpVerbMap;

        public OptionsResultPipe(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IHttpVerbMap httpVerbMap,
            IScopedStorage<HttpContext> httpContextStorage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.verbGeneration = verbGeneration;
            this.httpVerbMap = httpVerbMap;
            this.httpContextStorage = httpContextStorage;
        }

        protected override IActionResult CreateResult(TInput source)
        {
            this.SetAllowHeader(source);
            return new OkResult();
        }

        private void SetAllowHeader(TInput input)
        {
            var verbs = this.verbGeneration(input)
                .Select(v => this.httpVerbMap[v])
                .Aggregate(
                    new StringBuilder("OPTIONS"),
                    (current, next) => current.Append($", {next}"))
                .ToString();
            this.httpContextStorage.Value.Response.Headers
                .Append("Allow", new StringValues(verbs));
        }
    }
}
