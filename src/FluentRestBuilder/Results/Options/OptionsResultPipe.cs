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

    public class OptionsResultPipe<TInput> : ResultPipe<TInput>
        where TInput : class
    {
        private static readonly Dictionary<HttpVerb, string> VerbLookup
            = new Dictionary<HttpVerb, string>
            {
                { HttpVerb.Delete, "DELETE" },
                { HttpVerb.Get, "GET, HEAD" },
                { HttpVerb.Patch, "PATCH" },
                { HttpVerb.Post, "POST" },
                { HttpVerb.Put, "PUT" }
            };

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Func<TInput, IEnumerable<HttpVerb>> verbGeneration;

        public OptionsResultPipe(
            Func<TInput, IEnumerable<HttpVerb>> verbGeneration,
            IHttpContextAccessor httpContextAccessor,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.verbGeneration = verbGeneration;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override IActionResult CreateResult(TInput source)
        {
            this.SetAllowHeader(source);
            return new OkResult();
        }

        private void SetAllowHeader(TInput input)
        {
            var verbs = this.verbGeneration(input)
                .Select(v => VerbLookup[v])
                .Aggregate(
                    new StringBuilder("OPTIONS"),
                    (current, next) => current.Append($", {next}"))
                .ToString();
            this.httpContextAccessor.HttpContext.Response.Headers
                .Append("Allow", new StringValues(verbs));
        }
    }
}
