// <copyright file="RestCollectionPagination.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.Pagination
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.RestCollectionMutators.Pagination;
    using Microsoft.AspNetCore.Http;

    public class RestCollectionPagination<TEntity> : IRestCollectionPagination<TEntity>
    {
        private const int FirstPage = 1;
        private const int MinimumEntriesPerPage = 1;
        private readonly Lazy<int> actualEntriesPerPage;
        private readonly Lazy<int> actualPage;
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public RestCollectionPagination(
            IQueryCollection queryCollection,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryCollection = queryCollection;
            this.queryArgumentKeys = queryArgumentKeys;
            this.actualPage = new Lazy<int>(this.ResolvePage);
            this.actualEntriesPerPage = new Lazy<int>(this.ResolveEntriesPerPage);
        }

        public PaginationOptions Options { get; set; } = new PaginationOptions();

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
        {
            var skipAmount = (this.actualPage.Value - 1) * this.actualEntriesPerPage.Value;
            return queryable.Skip(skipAmount).Take(this.actualEntriesPerPage.Value);
        }

        private int ResolvePage()
        {
            var pageValue = this.queryCollection[this.queryArgumentKeys.Page];
            int page;
            return int.TryParse(pageValue.ToString(), out page) && page >= FirstPage
                ? page : FirstPage;
        }

        private int ResolveEntriesPerPage()
        {
            var entriesPerPageValue = this.queryCollection[this.queryArgumentKeys.EntriesPerPage];
            int entriesPerPage;
            return int.TryParse(entriesPerPageValue.ToString(), out entriesPerPage)
                   && entriesPerPage >= MinimumEntriesPerPage
                   && entriesPerPage <= this.Options.MaxEntriesPerPage
                ? entriesPerPage
                : this.Options.DefaultEntriesPerPage;
        }
    }
}