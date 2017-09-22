// <copyright file="IOrderByExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    public interface IOrderByExpressionFactory<TEntity>
    {
        IOrderByExpression<TEntity> Create(OrderByDirection direction);
    }
}
