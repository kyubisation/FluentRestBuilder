// <copyright file="FilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class FilterExpressionProvider<TEntity, TFilter> : IFilterExpressionProvider<TEntity>
    {
        private readonly IDictionary<FilterType, Func<TFilter, Expression<Func<TEntity, bool>>>> filterDictionary;

        protected FilterExpressionProvider(
            IDictionary<FilterType, Func<TFilter, Expression<Func<TEntity, bool>>>> filterDictionary)
        {
            this.filterDictionary = filterDictionary;
        }

        public abstract Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter);

        protected Expression<Func<TEntity, bool>> ResolveForType(FilterType type, TFilter filter) =>
            this.filterDictionary.TryGetValue(type, out var expressionCreator)
                ? expressionCreator(filter) : null;
    }
}