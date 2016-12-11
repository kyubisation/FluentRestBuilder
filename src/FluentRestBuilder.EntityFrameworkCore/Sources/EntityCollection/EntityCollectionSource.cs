// <copyright file="EntityCollectionSource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Sources.EntityCollection
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Common;
    using Core;
    using Core.Common;
    using Core.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using RestCollectionMutators.Common;
    using RestCollectionMutators.Filter;
    using RestCollectionMutators.OrderBy;
    using RestCollectionMutators.Pagination;
    using RestCollectionMutators.Search;

    public class EntityCollectionSource<TEntity> :
        EntitySource<TEntity, EntityCollectionSource<TEntity>>,
        IOutputPipe<IQueryable<TEntity>>
        where TEntity : class
    {
        private readonly IScopedStorage<PaginationMetaInfo> storage;
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<IQueryable<TEntity>> child;
        private IRestCollectionFilter<TEntity> collectionFilter;
        private IRestCollectionOrderBy<TEntity> collectionOrderBy;
        private IRestCollectionPagination<TEntity> collectionPagination;
        private IRestCollectionSearch<TEntity> collectionSearch;

        public EntityCollectionSource(
            IQueryable<TEntity> queryable,
            IScopedStorage<PaginationMetaInfo> storage,
            IServiceProvider serviceProvider)
            : base(queryable)
        {
            this.storage = storage;
            this.serviceProvider = serviceProvider;
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        TPipe IOutputPipe<IQueryable<TEntity>>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        async Task<IActionResult> IPipe.Execute()
        {
            return await this.ExecuteAsync();
        }

        public EntityCollectionSource<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            this.Queryable = this.Queryable.Where(predicate);
            return this;
        }

        public EntityCollectionSource<TEntity> ApplyFilter(IRestCollectionFilter<TEntity> filter)
        {
            this.collectionFilter = filter;
            return this;
        }

        public EntityCollectionSource<TEntity> ApplyFilter() =>
            this.ApplyFilter(this.GetRequiredService<IRestCollectionFilter<TEntity>>());

        public EntityCollectionSource<TEntity> ApplyOrderBy(IRestCollectionOrderBy<TEntity> orderBy)
        {
            this.collectionOrderBy = orderBy;
            return this;
        }

        public EntityCollectionSource<TEntity> ApplyOrderBy() =>
            this.ApplyOrderBy(this.GetRequiredService<IRestCollectionOrderBy<TEntity>>());

        public EntityCollectionSource<TEntity> ApplyPagination(
            IRestCollectionPagination<TEntity> pagination)
        {
            this.collectionPagination = pagination;
            return this;
        }

        public EntityCollectionSource<TEntity> ApplyPagination() =>
            this.ApplyPagination(this.GetRequiredService<IRestCollectionPagination<TEntity>>());

        public EntityCollectionSource<TEntity> ApplySearch(IRestCollectionSearch<TEntity> search)
        {
            this.collectionSearch = search;
            return this;
        }

        public EntityCollectionSource<TEntity> ApplySearch() =>
            this.ApplySearch(this.GetRequiredService<IRestCollectionSearch<TEntity>>());

        protected async Task<IActionResult> ExecuteAsync()
        {
            NoPipeAttachedException.Check(this.child);
            var mutators = new IRestCollectionMutator<TEntity>[]
            {
                this.collectionSearch,
                this.collectionFilter,
                this.collectionOrderBy
            }.Where(m => m != null);
            var queryable = mutators.Aggregate(
                this.Queryable, (current, next) => next.Apply(current));
            if (this.collectionPagination != null)
            {
                this.FillPaginationMetaInfo(queryable);
                queryable = this.collectionPagination.Apply(queryable);
            }

            return await this.child.Execute(queryable);
        }

        private void FillPaginationMetaInfo(IQueryable<TEntity> queryable)
        {
            var totalEntities = queryable.Count();
            var totalPages = (double)totalEntities / this.collectionPagination.ActualEntriesPerPage;
            this.storage.Value = new PaginationMetaInfo(
                totalEntities,
                this.collectionPagination.ActualPage,
                this.collectionPagination.ActualEntriesPerPage,
                (int)Math.Ceiling(totalPages));
        }
    }
}
