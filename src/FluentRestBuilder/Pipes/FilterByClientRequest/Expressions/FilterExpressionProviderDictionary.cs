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

        public FilterExpressionProviderDictionary(
            IServiceProvider serviceProvider,
            IEqualityComparer<string> equalityComparer)
            : base(equalityComparer)
        {
            this.serviceProvider = serviceProvider;
        }

        public FilterExpressionProviderDictionary(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Add a filter for a field/property with the string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// returning a dictionary of supported filter types with appropriate implementation.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddFilter(
            string property,
            Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder)
        {
            var expressionProvider = new StringFilterExpressionProvider<TEntity>(builder);
            this.Add(property, expressionProvider);
            return this;
        }

        /// <summary>
        /// Add a filter for a field/property with the string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// <see cref="FilterExpressionDictionary{TEntity}"/> to create the appropriate
        /// filter type implementations.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddFilter(
                string property,
                Func<
                    string,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddFilter(property, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        /// <summary>
        /// Add a filter for a field/property with a non-string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// <see cref="FilterExpressionDictionary{TEntity}"/> to create the appropriate
        /// filter type implementations.
        /// 
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
                string property,
                Func<
                    TFilter,
                    FilterExpressionDictionary<TEntity>,
                    IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter<TFilter>(
                property, f => builder(f, new FilterExpressionDictionary<TEntity>()));

        /// <summary>
        /// Add a filter for a field/property with a non-string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// returning a dictionary of supported filter types with appropriate implementation.
        /// 
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
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
