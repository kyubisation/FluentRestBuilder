// <copyright file="AllowedOptionsBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ActionResult.Options
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    public class AllowedOptionsBuilder<TSource>
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly List<Tuple<Func<ClaimsPrincipal, TSource, bool>, IEnumerable<HttpVerb>>> verbChecks
            = new List<Tuple<Func<ClaimsPrincipal, TSource, bool>, IEnumerable<HttpVerb>>>();

        public AllowedOptionsBuilder(ClaimsPrincipal claimsPrincipal)
        {
            Check.IsNull(claimsPrincipal, nameof(claimsPrincipal));
            this.claimsPrincipal = claimsPrincipal;
        }

        public AllowedOptionsBuilder<TSource> IsAllowed(
            IEnumerable<HttpVerb> verbs, Func<ClaimsPrincipal, TSource, bool> validCheck)
        {
            this.verbChecks.Add(Tuple.Create(validCheck, verbs));
            return this;
        }

        public IEnumerable<HttpVerb> GenerateAllowedVerbs(TSource input) =>
            this.verbChecks
                .Where(c => c.Item1(this.claimsPrincipal, input))
                .SelectMany(c => c.Item2)
                .Distinct()
                .OrderBy(v => v);
    }
}
