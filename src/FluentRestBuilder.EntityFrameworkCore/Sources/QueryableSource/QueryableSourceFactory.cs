// <copyright file="QueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using EntityFrameworkCore.Common;

    public class QueryableSourceFactory<TEntity> : IQueryableSourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;
        private readonly IServiceProvider serviceProvider;

        public QueryableSourceFactory(
            IQueryableFactory<TEntity> queryableFactory,
            IServiceProvider serviceProvider)
        {
            this.queryableFactory = queryableFactory;
            this.serviceProvider = serviceProvider;
        }

        public QueryableSource<TEntity> Resolve() =>
            new QueryableSource<TEntity>(this.queryableFactory, this.serviceProvider);
    }
}