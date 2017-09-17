// <copyright file="OptionsResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ActionResult.Options
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    public class OptionsResult : OkResult
    {
        private static readonly Dictionary<HttpVerb, string> VerbMap = new Dictionary<HttpVerb, string>
        {
            [HttpVerb.Delete] = "DELETE",
            [HttpVerb.Get] = "GET, HEAD",
            [HttpVerb.Patch] = "PATCH",
            [HttpVerb.Post] = "POST",
            [HttpVerb.Put] = "PUT",
        };

        private readonly string verbs;

        public OptionsResult(IEnumerable<HttpVerb> verbs)
        {
            var verbStrings = verbs
                .Select(v => VerbMap.TryGetValue(v, out var verbString) ? verbString : null)
                .Where(v => !string.IsNullOrWhiteSpace(v));
            this.verbs = string.Join(", ", verbStrings);
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.Headers.Append("Allow", new StringValues(this.verbs));
        }
    }
}
