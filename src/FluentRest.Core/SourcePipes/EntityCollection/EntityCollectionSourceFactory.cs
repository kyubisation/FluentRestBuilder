// <copyright file="EntityCollectionSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.SourcePipes.EntityCollection
{
    using System;
    using Core.Common;

    public class EntityCollectionSourceFactory<TEntity> : IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;
        private readonly IServiceProvider serviceProvider;

        public EntityCollectionSourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
        }

        public EntityCollectionSource<TEntity> Resolve() =>
            new EntityCollectionSource<TEntity>(
                this.queryableFactory.Queryable, this.serviceProvider);
    }
}
