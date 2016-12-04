// <copyright file="IFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.MetaModel.Filters
{
    using System;
    using System.Linq.Expressions;

    public interface IFilterBuilder<TEntity>
    {
        Expression<Func<TEntity, bool>> CreateFilter(object filter);
    }
}