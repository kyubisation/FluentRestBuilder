// <copyright file="PredicateBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class PredicateBuilder<TEntity> : IPredicateBuilder<TEntity>
    {
        public ParameterExpression Parameter { get; } = Expression.Parameter(typeof(TEntity));

        public Expression<Func<TEntity, bool>> JoinExpressionsByOr(
            params Expression<Func<TEntity, bool>>[] expressions) => expressions.Aggregate(Or);

        public Expression<Func<TEntity, bool>> JoinExpressionsByAnd(
            params Expression<Func<TEntity, bool>>[] expressions) => expressions.Aggregate(And);

        private static Expression<Func<TEntity, bool>> Or(
            Expression<Func<TEntity, bool>> expr1, Expression<Func<TEntity, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static Expression<Func<TEntity, bool>> And(
            Expression<Func<TEntity, bool>> expr1, Expression<Func<TEntity, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}