// <copyright file="OrderByExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class OrderByExpressionFactory<TEntity, TKey> : IOrderByExpressionFactory<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> orderBy;

        public OrderByExpressionFactory(Expression<Func<TEntity, TKey>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public IOrderByExpression<TEntity> Create(OrderByDirection direction)
        {
            switch (direction)
            {
                case OrderByDirection.Descending:
                    return new DescendingOrderByExpression<TEntity, TKey>(this.orderBy);
                default:
                    return new AscendingOrderByExpression<TEntity, TKey>(this.orderBy);
            }
        }
    }
}