// <copyright file="IQueryableTransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IQueryableTransformer<TEntity>
    {
        Task<List<TEntity>> ToList(IQueryable<TEntity> queryable);

        Task<TEntity> SingleOrDefault(IQueryable<TEntity> queryable);

        Task<TEntity> FirstOrDefault(IQueryable<TEntity> queryable);

        Task<int> Count(IQueryable<TEntity> queryable);
    }
}
