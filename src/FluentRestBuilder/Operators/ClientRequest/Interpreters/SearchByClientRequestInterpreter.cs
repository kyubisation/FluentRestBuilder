﻿// <copyright file="SearchByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using Microsoft.AspNetCore.Http;
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
            return queryCollection.TryGetValue(this.SearchQueryArgumentKey, out var search)
                && !string.IsNullOrEmpty(search)
                ? search.ToString() : null;
        }
    }
}