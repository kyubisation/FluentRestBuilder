// <copyright file="SearchByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class SearchByClientRequestPipeFactory<TInput> : ISearchByClientRequestPipeFactory<TInput>
    {
        private readonly IScopedStorage<HttpContext> httpContextStorage;
        private readonly IQueryArgumentKeys queryArgumentKeys;

        public SearchByClientRequestPipeFactory(
            IScopedStorage<HttpContext> httpContextStorage,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.httpContextStorage = httpContextStorage;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public OutputPipe<IQueryable<TInput>> Resolve(
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new SearchByClientRequestPipe<TInput>(
                this.httpContextStorage, this.queryArgumentKeys, search, parent);
    }
}