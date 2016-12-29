// <copyright file="IFilterExpressionProviderBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IFilterExpressionProviderBuilder<TEntity>
    {
        IFilterExpressionProviderBuilder<TEntity> AddEquals(
            Expression<Func<TEntity, bool>> expression);

        IFilterExpressionProviderBuilder<TEntity> AddContains(
            Expression<Func<TEntity, bool>> expression);

        IFilterExpressionProviderBuilder<TEntity> AddGreaterThan(
            Expression<Func<TEntity, bool>> expression);

        IFilterExpressionProviderBuilder<TEntity> AddGreaterThanOrEqual(
            Expression<Func<TEntity, bool>> expression);

        IFilterExpressionProviderBuilder<TEntity> AddLessThan(
            Expression<Func<TEntity, bool>> expression);

        IFilterExpressionProviderBuilder<TEntity> AddLessThanOrEqual(
            Expression<Func<TEntity, bool>> expression);

        IDictionary<FilterType, Expression<Func<TEntity, bool>>> Build();
    }
}
