// <copyright file="GenericFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class GenericFilterExpressionProvider<TEntity, TFilter> : IFilterExpressionProvider<TEntity>
    {
        private readonly Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder;
        private readonly Func<string, TFilter> conversion;

        public GenericFilterExpressionProvider(
            Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder,
            Func<string, TFilter> conversion)
        {
            this.filterBuilder = filterBuilder;
            this.conversion = conversion;
        }

        public Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter)
        {
            try
            {
                return this.TryResolve(type, filter);
            }
            catch (Exception)
            {
                return e => false;
            }
        }

        private Expression<Func<TEntity, bool>> TryResolve(FilterType type, string filter)
        {
            try
            {
                var filterValue = this.conversion(filter);
                var dictionary = this.filterBuilder(filterValue);
                Expression<Func<TEntity, bool>> filterExpression;
                return dictionary.TryGetValue(type, out filterExpression) ? filterExpression : null;
            }
            catch (Exception) when (!this.filterBuilder(default(TFilter)).ContainsKey(type))
            {
                return null;
            }
        }
    }
}