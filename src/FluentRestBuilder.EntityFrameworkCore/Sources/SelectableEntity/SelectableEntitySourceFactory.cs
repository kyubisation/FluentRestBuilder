// <copyright file="SelectableEntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.SelectableEntity
{
    using System;
    using System.Linq.Expressions;
    using EntityFrameworkCore.Common;
    using Storage;

    public class SelectableEntitySourceFactory<TEntity> : ISelectableEntitySourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;
        private readonly IScopedStorage<TEntity> entityStorage;
        private readonly IServiceProvider serviceProvider;

        public SelectableEntitySourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory,
            IScopedStorage<TEntity> entityStorage)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
            this.entityStorage = entityStorage;
        }

        public SelectableEntitySource<TEntity> Resolve(
            Expression<Func<TEntity, bool>> selectionFilter) =>
            new SelectableEntitySource<TEntity>(
                selectionFilter,
                this.queryableFactory.Queryable,
                this.entityStorage,
                this.serviceProvider);
    }
}
