// <copyright file="IOrderByExpressionBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IOrderByExpressionBuilder<TEntity>
    {
        IOrderByExpressionBuilder<TEntity> Add<TKey>(
            string key, Expression<Func<TEntity, TKey>> orderByExpression);

        IDictionary<string, IOrderByExpressionFactory<TEntity>> Build();

        IDictionary<string, IOrderByExpressionFactory<TEntity>> BuildCaseInsensitive();
    }
}
