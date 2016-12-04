// <copyright file="SelectableEntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Sources.SelectableEntity
{
    using System;
    using System.Linq.Expressions;
    using Common;

    public class SelectableEntitySourceFactory<TEntity> : ISelectableEntitySourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;
        private readonly IServiceProvider serviceProvider;

        public SelectableEntitySourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
        }

        public SelectableEntitySource<TEntity> Resolve(
            Expression<Func<TEntity, bool>> selectionFilter) =>
            new SelectableEntitySource<TEntity>(
                selectionFilter, this.queryableFactory.Queryable, this.serviceProvider);
    }
}
