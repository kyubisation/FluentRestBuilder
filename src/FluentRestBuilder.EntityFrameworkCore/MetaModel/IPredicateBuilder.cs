// <copyright file="IPredicateBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Linq.Expressions;

    public interface IPredicateBuilder<TEntity>
    {
        ParameterExpression Parameter { get; }

        Expression<Func<TEntity, bool>> JoinExpressionsByOr(
            params Expression<Func<TEntity, bool>>[] expressions);

        Expression<Func<TEntity, bool>> JoinExpressionsByAnd(
            params Expression<Func<TEntity, bool>>[] expressions);
    }
}
