// <copyright file="FilterExpressionProviderDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;

    public class FilterExpressionProviderDictionary<TEntity> : Dictionary<string, IFilterExpressionProvider<TEntity>>
    {
        public FilterExpressionProviderDictionary<TEntity> AddFilter(
            string property,
            Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder)
        {
            var expressionProvider = new StringFilterExpressionProvider<TEntity>(builder);
            this.Add(property, expressionProvider);
            return this;
        }

        public FilterExpressionProviderDictionary<TEntity> AddFilter(
                string property,
                Func<
                    string,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddFilter(property, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
                string property,
                Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter(property, ConvertToFilterType<TFilter>, builder);

        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
                string property,
                Func<
                    TFilter,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter<TFilter>(
                property, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
            string property,
            Func<string, TFilter> conversion,
            Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder)
        {
            var expressionProvider = new GenericFilterExpressionProvider<TEntity, TFilter>(
                builder, conversion ?? ConvertToFilterType<TFilter>);
            this.Add(property, expressionProvider);
            return this;
        }

        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
                string property,
                Func<string, TFilter> conversion,
                Func<
                    TFilter,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter(
                property, conversion, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        private static TFilter ConvertToFilterType<TFilter>(string filter)
        {
            try
            {
                return (TFilter)Convert.ChangeType(filter, typeof(TFilter));
            }
            catch (Exception)
            {
                return (TFilter)Convert.ChangeType(
                    filter, typeof(TFilter), CultureInfo.InvariantCulture);
            }
        }
    }
}
