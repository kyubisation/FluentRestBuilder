// <copyright file="FilterExpressionProviderDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Converters;
    using Microsoft.Extensions.DependencyInjection;

    public class FilterExpressionProviderDictionary<TEntity> : Dictionary<string, IFilterExpressionProvider<TEntity>>
    {
        private readonly IServiceProvider serviceProvider;

        public FilterExpressionProviderDictionary(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

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
                Func<
                    TFilter,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter<TFilter>(
                property, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
            string property,
            Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder)
        {
            var converter = this.serviceProvider.GetService<IFilterToTypeConverter<TFilter>>();
            var expressionProvider = new GenericFilterExpressionProvider<TEntity, TFilter>(
                builder, converter);
            this.Add(property, expressionProvider);
            return this;
        }
    }
}
