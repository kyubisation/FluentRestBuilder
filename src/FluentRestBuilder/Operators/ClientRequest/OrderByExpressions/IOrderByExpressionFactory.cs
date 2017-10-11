// <copyright file="IOrderByExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using Interpreters.Requests;

    public interface IOrderByExpressionFactory<TEntity>
    {
        IOrderByExpression<TEntity> Create(OrderByDirection direction);
    }
}
