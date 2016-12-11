// <copyright file="IAllowedOptionsBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.Options
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IAllowedOptionsBuilder<TInput>
    {
        IAllowedOptionsBuilder<TInput> IsAllowed(
            IEnumerable<HttpVerb> verbs, Func<ClaimsPrincipal, TInput, bool> validCheck);

        IEnumerable<HttpVerb> GenerateAllowedVerbs(TInput input);
    }
}
