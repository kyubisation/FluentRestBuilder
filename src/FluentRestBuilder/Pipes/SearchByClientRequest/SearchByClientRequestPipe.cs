// <copyright file="SearchByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public class SearchByClientRequestPipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly Func<string, Expression<Func<TInput, bool>>> search;
        private readonly IQueryCollection queryCollection;

        public SearchByClientRequestPipe(
            IHttpContextAccessor httpContextAccessor,
            IQueryArgumentKeys queryArgumentKeys,
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.queryArgumentKeys = queryArgumentKeys;
            this.search = search;
            this.queryCollection = httpContextAccessor.HttpContext.Request.Query;
        }

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            StringValues searchValue;
            return !this.queryCollection.TryGetValue(this.queryArgumentKeys.Search, out searchValue)
                ? input : this.ApplySearch(input, searchValue.ToString());
        }

        private IQueryable<TInput> ApplySearch(IQueryable<TInput> queryable, string searchValue) =>
            queryable.Where(this.search(searchValue));
    }
}
