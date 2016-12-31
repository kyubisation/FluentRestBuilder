// <copyright file="IQueryableFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.QueryableFactories
{
    using System.Linq;

    public interface IQueryableFactory
    {
        IQueryable<TEntity> Resolve<TEntity>()
            where TEntity : class;
    }

    public interface IQueryableFactory<out TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }
    }
}
