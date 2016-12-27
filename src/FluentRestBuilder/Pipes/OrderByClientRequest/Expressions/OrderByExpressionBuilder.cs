// <copyright file="OrderByExpressionBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class OrderByExpressionBuilder<TEntity> : IOrderByExpressionBuilder<TEntity>
    {
        private readonly IDictionary<string, IOrderByExpressionFactory<TEntity>> orderByDictionary =
            new Dictionary<string, IOrderByExpressionFactory<TEntity>>();

        public IOrderByExpressionBuilder<TEntity> Add<TKey>(
            string key, Expression<Func<TEntity, TKey>> orderByExpression)
        {
            this.orderByDictionary.Add(
                key, new OrderByExpressionFactory<TEntity, TKey>(orderByExpression));
            return this;
        }

        public IDictionary<string, IOrderByExpressionFactory<TEntity>> Build() =>
            this.orderByDictionary;

        public IDictionary<string, IOrderByExpressionFactory<TEntity>> BuildCaseInsensitive() =>
            new Dictionary<string, IOrderByExpressionFactory<TEntity>>(
                this.orderByDictionary, StringComparer.OrdinalIgnoreCase);
    }
}