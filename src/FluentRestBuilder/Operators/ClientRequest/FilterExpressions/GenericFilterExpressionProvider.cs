// <copyright file="GenericFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FilterConverters;
    using Interpreters.Requests;

    public class GenericFilterExpressionProvider<TEntity, TFilter> : IFilterExpressionProvider<TEntity>
    {
        private readonly Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder;
        private readonly IFilterToTypeConverter<TFilter> converter;

        public GenericFilterExpressionProvider(
            Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> filterBuilder,
            IFilterToTypeConverter<TFilter> converter)
        {
            this.filterBuilder = filterBuilder;
            this.converter = converter;
        }

        public Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter)
        {
            try
            {
                return this.TryResolve(type, filter);
            }
            catch (Exception)
            {
                return this.CheckForExistingFilterOnError(type);
            }
        }

        private Expression<Func<TEntity, bool>> TryResolve(FilterType type, string filter)
        {
            var conversionResult = this.converter.Parse(filter);
            if (conversionResult.Success)
            {
                return this.BuildFilter(type, conversionResult.Value);
            }

            return this.CheckForExistingFilterOnError(type);
        }

        private Expression<Func<TEntity, bool>> BuildFilter(FilterType type, TFilter filter)
        {
            var dictionary = this.filterBuilder(filter);
            return dictionary.TryGetValue(type, out var filterExpression) ? filterExpression : null;
        }

        private Expression<Func<TEntity, bool>> CheckForExistingFilterOnError(FilterType type)
        {
            if (this.filterBuilder(default(TFilter)).ContainsKey(type))
            {
                return e => false;
            }

            return null;
        }
    }
}