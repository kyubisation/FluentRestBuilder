// <copyright file="QueryableFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Common
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class QueryableFactory : IQueryableFactory
    {
        private readonly DbContext context;

        public QueryableFactory(DbContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Resolve<TEntity>()
            where TEntity : class =>
            this.context.Set<TEntity>();
    }

#pragma warning disable SA1402 // File may only contain a single class
    public class QueryableFactory<TEntity> : IQueryableFactory<TEntity>
#pragma warning restore SA1402 // File may only contain a single class
        where TEntity : class
    {
        public QueryableFactory(IQueryableFactory queryableFactory)
        {
            this.Queryable = queryableFactory.Resolve<TEntity>();
        }

        public IQueryable<TEntity> Queryable { get; }
    }
}