// <copyright file="StringFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Interpreters.Requests;

    public class StringFilterExpressionProvider<TEntity> : IFilterExpressionProvider<TEntity>
    {
        private readonly Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder;

        public StringFilterExpressionProvider(
            Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder)
        {
            this.filterBuilder = filterBuilder;
        }

        public StringFilterExpressionProvider(string propertyName)
            : this(filter => CreateDefault(propertyName, filter))
        {
        }

        public Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter)
        {
            var dictionary = this.filterBuilder(filter);
            return dictionary.TryGetValue(type, out var filterExpression) ? filterExpression : null;
        }

        private static IDictionary<FilterType, Expression<Func<TEntity, bool>>> CreateDefault(
            string propertyName, string filter)
        {
            var builder = new FilterExpressionBuilder<TEntity, string>(propertyName, filter);
            return new Dictionary<FilterType, Expression<Func<TEntity, bool>>>
            {
                [FilterType.Default] = builder.CreateContainsExpression(),
                [FilterType.Contains] = builder.CreateContainsExpression(),
                [FilterType.Equals] = builder.CreateEqualsExpression(),
                [FilterType.StartsWith] = builder.CreateStartsWithExpression(),
                [FilterType.EndsWith] = builder.CreateEndsWithExpression(),
                [FilterType.NotEqual] = builder.CreateNotEqualExpression(),
            };
        }
    }
}