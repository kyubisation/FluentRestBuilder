// <copyright file="OrderByExpressionDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class OrderByExpressionDictionary<TEntity> : Dictionary<string, IOrderByExpressionFactory<TEntity>>
    {
        public OrderByExpressionDictionary()
        {
        }

        public OrderByExpressionDictionary(IEqualityComparer<string> equalityComparer)
            : base(equalityComparer)
        {
        }

        /// <summary>
        /// The expression to be used for the order by.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <param name="key">The field/property name.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public OrderByExpressionDictionary<TEntity> Add<TKey>(
            string key, Expression<Func<TEntity, TKey>> orderByExpression)
        {
            this.Add(key, new OrderByExpressionFactory<TEntity, TKey>(orderByExpression));
            return this;
        }
    }
}