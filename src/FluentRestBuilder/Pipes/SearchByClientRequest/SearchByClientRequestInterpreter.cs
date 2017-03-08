// <copyright file="SearchByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Storage;

    public class SearchByClientRequestInterpreter : ISearchByClientRequestInterpreter
    {
        private readonly IScopedStorage<HttpContext> httpContextStorage;

        public SearchByClientRequestInterpreter(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.httpContextStorage = httpContextStorage;
        }

        public string SearchQueryArgumentKey { get; set; } = "q";

        public string ParseRequestQuery()
        {
            var queryCollection = this.httpContextStorage.Value.Request.Query;
            StringValues search;
            return queryCollection.TryGetValue(this.SearchQueryArgumentKey, out search)
                && !string.IsNullOrEmpty(search)
                ? search.ToString() : null;
        }
    }
}