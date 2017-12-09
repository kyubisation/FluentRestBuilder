// <copyright file="LinkHelper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;

    public class LinkHelper : ILinkHelper
    {
        private readonly HttpContext httpContext;

        public LinkHelper(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.httpContext = httpContextStorage.Value;
        }

        public string CurrentUrl() => this.httpContext.Request.GetDisplayUrl();

        public string ModifyCurrentUrl(IDictionary<string, string> modifications)
        {
            var queryParams = this.httpContext.Request.Query.Where(
                q => modifications.Keys.All(
                    k => !string.Equals(q.Key, k, StringComparison.OrdinalIgnoreCase)));
            var queryBuilder = new QueryBuilder();
            foreach (var keyValuePair in queryParams)
            {
                queryBuilder.Add(keyValuePair.Key, keyValuePair.Value.ToArray());
            }

            foreach (var modification in modifications.Where(m => m.Value != null))
            {
                queryBuilder.Add(modification.Key, modification.Value);
            }

            return new UriBuilder(this.httpContext.Request.GetDisplayUrl())
                {
                    Query = queryBuilder.ToString(),
                }
                .Uri.AbsoluteUri;
        }
    }
}
