// <copyright file="OrderByExpressionDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class OrderByExpressionDictionary<TEntity>
        : Dictionary<string, IOrderByExpressionFactory<TEntity>>, IOrderByExpressionDictionary<TEntity>
    {
        /// <summary>
        /// Configure order by logic for all public properties.
        /// </summary>
        /// <returns>
        /// Itself. An instance of <see cref="OrderByExpressionDictionary{TEntity}"/>.
        /// </returns>
        public OrderByExpressionDictionary<TEntity> AddProperties()
        {
            new PropertyReflectionHelper(this)
                .AddProperties();
            return this;
        }

        /// <summary>
        /// Configure an order by logic.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <param name="key">The field/property name.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        /// Itself. An instance of <see cref="OrderByExpressionDictionary{TEntity}"/>.
        /// </returns>
        public OrderByExpressionDictionary<TEntity> Add<TKey>(
            string key, Expression<Func<TEntity, TKey>> orderByExpression)
        {
            this.Add(key, new OrderByExpressionFactory<TEntity, TKey>(orderByExpression));
            return this;
        }

        /// <summary>
        /// Configure an order by logic.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        /// Itself. An instance of <see cref="OrderByExpressionDictionary{TEntity}"/>.
        /// </returns>
        public OrderByExpressionDictionary<TEntity> Add<TKey>(
            Expression<Func<TEntity, TKey>> orderByExpression) =>
            this.Add(orderByExpression.ToPropertyName(), orderByExpression);

        private sealed class PropertyReflectionHelper
        {
            private readonly OrderByExpressionDictionary<TEntity> dictionary;
            private readonly ParameterExpression parameter = Expression.Parameter(typeof(TEntity));

            public PropertyReflectionHelper(OrderByExpressionDictionary<TEntity> dictionary)
            {
                this.dictionary = dictionary;
            }

            public void AddProperties()
            {
                var properties = typeof(TEntity)
                    .GetRuntimeProperties()
                    .Where(p => p.GetGetMethod(true).IsPublic);
                var method = this.GetType()
                    .GetRuntimeMethod(nameof(this.AddProperty), new[] { typeof(string) });
                foreach (var property in properties)
                {
                    method.MakeGenericMethod(property.PropertyType)
                        .Invoke(this, new object[] { property.Name });
                }
            }

            // Must be public in order for reflection to work
            // ReSharper disable once MemberCanBePrivate.Local
            public void AddProperty<TProperty>(string name)
            {
                var expression = Expression.Lambda<Func<TEntity, TProperty>>(
                    Expression.Property(this.parameter, name), this.parameter);
                this.dictionary.Add(name, expression);
            }
        }
    }
}