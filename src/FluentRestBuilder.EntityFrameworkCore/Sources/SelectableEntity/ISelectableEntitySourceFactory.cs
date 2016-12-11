// <copyright file="ISelectableEntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.SelectableEntity
{
    using System;
    using System.Linq.Expressions;

    public interface ISelectableEntitySourceFactory<TEntity>
        where TEntity : class
    {
        SelectableEntitySource<TEntity> Resolve(Expression<Func<TEntity, bool>> selectionFilter);
    }
}