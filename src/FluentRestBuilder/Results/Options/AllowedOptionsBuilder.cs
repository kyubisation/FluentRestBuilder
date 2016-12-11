// <copyright file="AllowedOptionsBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class AllowedOptionsBuilder<TInput> : IAllowedOptionsBuilder<TInput>
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly List<Tuple<Func<ClaimsPrincipal, TInput, bool>, IEnumerable<HttpVerb>>> verbChecks
            = new List<Tuple<Func<ClaimsPrincipal, TInput, bool>, IEnumerable<HttpVerb>>>();

        public AllowedOptionsBuilder(IHttpContextAccessor httpContextAccessor)
        {
            this.claimsPrincipal = httpContextAccessor.HttpContext.User;
        }

        public IAllowedOptionsBuilder<TInput> IsAllowed(IEnumerable<HttpVerb> verbs, Func<ClaimsPrincipal, TInput, bool> validCheck)
        {
            this.verbChecks.Add(Tuple.Create(validCheck, verbs));
            return this;
        }

        public IEnumerable<HttpVerb> GenerateAllowedVerbs(TInput input) =>
            this.verbChecks
                .Where(c => c.Item1(this.claimsPrincipal, input))
                .SelectMany(c => c.Item2)
                .Distinct()
                .OrderBy(v => v);
    }
}
