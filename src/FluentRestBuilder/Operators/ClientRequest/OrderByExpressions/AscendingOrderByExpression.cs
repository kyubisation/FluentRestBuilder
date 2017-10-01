// <copyright file="AscendingOrderByExpression.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class AscendingOrderByExpression<TEntity, TKey> : IOrderByExpression<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> orderBy;

        public AscendingOrderByExpression(Expression<Func<TEntity, TKey>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable) =>
            queryable.OrderBy(this.orderBy);

        public IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> queryable) =>
            queryable.ThenBy(this.orderBy);
    }
}
