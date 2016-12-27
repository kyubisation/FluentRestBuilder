// <copyright file="SearchByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;
    using Microsoft.AspNetCore.Http;

    public class SearchByClientRequestPipeFactory<TInput> : ISearchByClientRequestPipeFactory<TInput>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IQueryArgumentKeys queryArgumentKeys;

        public SearchByClientRequestPipeFactory(
            IHttpContextAccessor httpContextAccessor,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public OutputPipe<IQueryable<TInput>> Resolve(
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new SearchByClientRequestPipe<TInput>(
                this.httpContextAccessor, this.queryArgumentKeys, search, parent);
    }
}