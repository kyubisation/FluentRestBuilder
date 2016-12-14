// <copyright file="IQueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    public interface IQueryableSourceFactory<TEntity>
        where TEntity : class
    {
        QueryableSource<TEntity> Resolve();
    }
}
