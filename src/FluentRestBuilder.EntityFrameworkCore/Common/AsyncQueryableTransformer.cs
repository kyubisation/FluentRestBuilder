// <copyright file="AsyncQueryableTransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Common;
    using Microsoft.EntityFrameworkCore;

    public class AsyncQueryableTransformer<TEntity> : IQueryableTransformer<TEntity>
    {
        public Task<List<TEntity>> ToList(IQueryable<TEntity> queryable) => queryable.ToListAsync();

        public Task<TEntity> SingleOrDefault(IQueryable<TEntity> queryable) =>
            queryable.SingleOrDefaultAsync();

        public Task<TEntity> FirstOrDefault(IQueryable<TEntity> queryable) =>
            queryable.FirstOrDefaultAsync();
    }
}
