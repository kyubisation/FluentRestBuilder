// <copyright file="StringFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class StringFilterExpressionProvider<TEntity> : IFilterExpressionProvider<TEntity>
    {
        private readonly Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder;

        public StringFilterExpressionProvider(
            Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder)
        {
            this.filterBuilder = filterBuilder;
        }

        public Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter)
        {
            var dictionary = this.filterBuilder(filter);
            Expression<Func<TEntity, bool>> filterExpression;
            return dictionary.TryGetValue(type, out filterExpression) ? filterExpression : null;
        }
    }
}