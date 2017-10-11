// <copyright file="OrderByExpressionDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class OrderByExpressionDictionary<TEntity>
        : Dictionary<string, IOrderByExpressionFactory<TEntity>>, IOrderByExpressionDictionary<TEntity>
    {
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

        /// <summary>
        /// The expression to be used for the order by.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public OrderByExpressionDictionary<TEntity> Add<TKey>(
            Expression<Func<TEntity, TKey>> orderByExpression)
        {
            var type = typeof(TEntity);
            var member = orderByExpression?.Body as MemberExpression;
            var propInfo = member?.Member as PropertyInfo;
            if (propInfo == null || !propInfo.ReflectedType.IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    "The provided order by expression is not a valid property expression!",
                    nameof(orderByExpression));
            }

            return this.Add(propInfo.Name, orderByExpression);
        }
    }
}