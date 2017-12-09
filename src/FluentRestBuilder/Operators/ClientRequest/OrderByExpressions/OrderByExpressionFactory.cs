// <copyright file="OrderByExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Interpreters.Requests;

    public class OrderByExpressionFactory<TEntity, TKey> : IOrderByExpressionFactory<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> orderBy;

        public OrderByExpressionFactory(Expression<Func<TEntity, TKey>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public IOrderByExpression<TEntity> Create(OrderByDirection direction)
        {
            if (direction == OrderByDirection.Descending)
            {
                return new DescendingOrderByExpression(this.orderBy);
            }

            return new AscendingOrderByExpression(this.orderBy);
        }

        private abstract class OrderByExpression : IOrderByExpression<TEntity>
        {
            private readonly Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy;
            private readonly Func<IOrderedQueryable<TEntity>, IOrderedQueryable<TEntity>> thenOrderBy;

            protected OrderByExpression(
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                Func<IOrderedQueryable<TEntity>, IOrderedQueryable<TEntity>> thenOrderBy)
            {
                this.orderBy = orderBy;
                this.thenOrderBy = thenOrderBy;
            }

            public IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable) =>
                this.orderBy(queryable);

            public IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> queryable) =>
                this.thenOrderBy(queryable);
        }

        private sealed class AscendingOrderByExpression : OrderByExpression
        {
            public AscendingOrderByExpression(Expression<Func<TEntity, TKey>> orderBy)
                : base(q => q.OrderBy(orderBy), q => q.ThenBy(orderBy))
            {
            }
        }

        private sealed class DescendingOrderByExpression : OrderByExpression
        {
            public DescendingOrderByExpression(Expression<Func<TEntity, TKey>> orderBy)
                : base(q => q.OrderByDescending(orderBy), q => q.ThenByDescending(orderBy))
            {
            }
        }
    }
}