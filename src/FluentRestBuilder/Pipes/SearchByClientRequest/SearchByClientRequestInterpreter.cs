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
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IScopedStorage<HttpContext> httpContextStorage;

        public SearchByClientRequestInterpreter(
            IQueryArgumentKeys queryArgumentKeys,
            IScopedStorage<HttpContext> httpContextStorage)
        {
            this.queryArgumentKeys = queryArgumentKeys;
            this.httpContextStorage = httpContextStorage;
        }

        public string ParseRequestQuery()
        {
            var queryCollection = this.httpContextStorage.Value.Request.Query;
            StringValues search;
            return queryCollection.TryGetValue(this.queryArgumentKeys.Search, out search)
                ? search.ToString() : null;
        }
    }
}