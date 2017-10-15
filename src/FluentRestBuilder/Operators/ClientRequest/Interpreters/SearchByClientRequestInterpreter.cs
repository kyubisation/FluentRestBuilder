// <copyright file="SearchByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using Json;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class SearchByClientRequestInterpreter : ISearchByClientRequestInterpreter
    {
        private readonly IScopedStorage<HttpContext> httpContextStorage;

        public SearchByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IJsonPropertyNameResolver jsonPropertyNameResolver)
        {
            this.httpContextStorage = httpContextStorage;
            this.SearchQueryArgumentKey = jsonPropertyNameResolver.Resolve("Q");
        }

        public string SearchQueryArgumentKey { get; set; }

        public string ParseRequestQuery()
        {
            var queryCollection = this.httpContextStorage.Value.Request.Query;
            return queryCollection.TryGetValue(this.SearchQueryArgumentKey, out var search)
                && !string.IsNullOrEmpty(search)
                ? search.ToString() : null;
        }
    }
}