// <copyright file="QueryableTransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QueryableTransformer<TEntity> : IQueryableTransformer<TEntity>
    {
        public Task<List<TEntity>> ToList(IQueryable<TEntity> queryable) =>
            Task.FromResult(queryable.ToList());

        public Task<TEntity> SingleOrDefault(IQueryable<TEntity> queryable) =>
            Task.FromResult(queryable.SingleOrDefault());

        public Task<TEntity> FirstOrDefault(IQueryable<TEntity> queryable) =>
            Task.FromResult(queryable.FirstOrDefault());

        public Task<int> Count(IQueryable<TEntity> queryable) =>
            Task.FromResult(queryable.Count());
    }
}