// <copyright file="EntityCollectionSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.EntityCollection
{
    using System;
    using EntityFrameworkCore.Common;
    using FluentRestBuilder.Common;
    using Storage;

    public class EntityCollectionSourceFactory<TEntity> : IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IServiceProvider serviceProvider;

        public EntityCollectionSourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
        }

        public EntityCollectionSource<TEntity> Resolve() =>
            new EntityCollectionSource<TEntity>(
                this.queryableFactory.Queryable, this.paginationMetaInfoStorage, this.serviceProvider);
    }
}
