// <copyright file="IEntityCollectionSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.SourcePipes.EntityCollection
{
    public interface IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        EntityCollectionSource<TEntity> Resolve();
    }
}