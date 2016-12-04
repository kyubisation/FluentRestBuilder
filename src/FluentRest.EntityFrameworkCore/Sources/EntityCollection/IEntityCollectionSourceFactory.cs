// <copyright file="IEntityCollectionSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Sources.EntityCollection
{
    public interface IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        EntityCollectionSource<TEntity> Resolve();
    }
}