// <copyright file="FilterExpressionProviderDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using FilterConverters;
    using Interpreters.Requests;
    using Microsoft.Extensions.DependencyInjection;

    public class FilterExpressionProviderDictionary<TEntity>
        : Dictionary<string, IFilterExpressionProvider<TEntity>>, IFilterExpressionProviderDictionary<TEntity>
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly Type[] BooleanTypes = { typeof(bool), typeof(bool?) };
        private static readonly Type[] DateTimeTypes =
        {
            typeof(DateTime), typeof(DateTime?), typeof(DateTimeOffset), typeof(DateTimeOffset?),
        };
        //// ReSharper restore StaticMemberInGenericType

        private readonly IServiceProvider serviceProvider;

        public FilterExpressionProviderDictionary(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Add a filter for each field/property with a string type or value type.
        /// Configures default filters for their respective types.
        /// </summary>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddPropertyFilters() =>
            typeof(TEntity)
                .GetRuntimeProperties()
                .Where(p => p.GetGetMethod(true).IsPublic)
                .Aggregate(this, (current, next) => current.AddPropertyFilter(next));

        /// <summary>
        /// Add a filter for a field/property with the string type.
        /// Configures Equals, NotEqual, Contains, StartsWith and EndsWith filters
        /// with Contains being the default.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddStringFilter(
            Expression<Func<TEntity, string>> propertySelector) =>
            this.AddStringFilter(propertySelector.ToPropertyName());

        /// <summary>
        /// Add a filter for a field/property with the string type.
        /// Configures Equals, NotEqual, Contains, StartsWith and EndsWith filters
        /// with Contains being the default.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddStringFilter(string property)
        {
            var expressionProvider = new StringFilterExpressionProvider<TEntity>(property);
            this.Add(property, expressionProvider);
            return this;
        }

        /// <summary>
        /// Add a filter for a field/property with a numeric type.
        /// Configures Equals, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan and
        /// LessThanOrEqual filters with Equals being the default.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddNumericFilter<TNumeric>(
            Expression<Func<TEntity, TNumeric>> propertySelector) =>
            this.AddNumericFilter<TNumeric>(propertySelector.ToPropertyName());

        /// <summary>
        /// Add a filter for a field/property with a numeric type.
        /// Configures Equals, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan and
        /// LessThanOrEqual filters with Equals being the default.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddNumericFilter<TNumeric>(
            string property)
        {
            var converter = this.serviceProvider.GetService<IFilterToTypeConverter<TNumeric>>();
            var expressionProvider = new NumericFilterExpressionProvider<TEntity, TNumeric>(
                property, converter);
            this.Add(property, expressionProvider);
            return this;
        }

        /// <summary>
        /// Add a filter for a field/property with a datetime type.
        /// Configures Equals, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan and
        /// LessThanOrEqual filters with Equals being the default.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddDateTimeFilter<TDateTime>(
            Expression<Func<TEntity, TDateTime>> propertySelector)
            where TDateTime : struct =>
            this.AddNumericFilter(propertySelector);

        /// <summary>
        /// Add a filter for a field/property with a datetime type.
        /// Configures Equals, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan and
        /// LessThanOrEqual filters with Equals being the default.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddDateTimeFilter<TDateTime>(
            string property)
            where TDateTime : struct =>
            this.AddNumericFilter<TDateTime>(property);

        /// <summary>
        /// Add a filter for a field/property with a boolean type.
        /// Configures Equals and NotEqual with Equals being the default.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddBooleanFilter<TBoolean>(
            Expression<Func<TEntity, TBoolean>> propertySelector) =>
            this.AddBooleanFilter<TBoolean>(propertySelector.ToPropertyName());

        /// <summary>
        /// Add a filter for a field/property with a boolean type.
        /// Configures Equals and NotEqual with Equals being the default.
        /// </summary>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddBooleanFilter<TBoolean>(
            string property)
        {
            var converter = this.serviceProvider.GetService<IFilterToTypeConverter<TBoolean>>();
            var expressionProvider = new BooleanFilterExpressionProvider<TEntity, TBoolean>(
                property, converter);
            this.Add(property, expressionProvider);
            return this;
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
        /// returning a dictionary of supported filter types with appropriate implementation.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddFilter<TProperty>(
            Expression<Func<TEntity, TProperty>> propertySelector,
            Func<string, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddFilter(propertySelector.ToPropertyName(), builder);

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
        /// Add a filter for a field/property with the string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// <see cref="FilterExpressionDictionary{TEntity}"/> to create the appropriate
        /// filter type implementations.
        /// </summary>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddFilter<TProperty>(
            Expression<Func<TEntity, TProperty>> propertySelector,
            Func<
                string,
                FilterExpressionDictionary<TEntity>,
                IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddFilter(propertySelector.ToPropertyName(), builder);

        /// <summary>
        /// Add a filter for a field/property with a non-string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// <see cref="FilterExpressionDictionary{TEntity}"/> to create the appropriate
        /// filter type implementations.
        /// <para>
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </para>
        /// </summary>
        /// <typeparam name="TFilter">The filter type.</typeparam>
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
        /// <see cref="FilterExpressionDictionary{TEntity}"/> to create the appropriate
        /// filter type implementations.
        /// <para>
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </para>
        /// </summary>
        /// <typeparam name="TFilter">The filter type.</typeparam>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
            Expression<Func<TEntity, TFilter>> propertySelector,
            Func<
                TFilter,
                FilterExpressionDictionary<TEntity>,
                IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter(propertySelector.ToPropertyName(), builder);

        /// <summary>
        /// Add a filter for a field/property with a non-string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// returning a dictionary of supported filter types with appropriate implementation.
        /// <para>
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </para>
        /// </summary>
        /// <typeparam name="TFilter">The filter type.</typeparam>
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

        /// <summary>
        /// Add a filter for a field/property with a non-string type.
        /// Provide a factory function receiving the filter value for this field/property and
        /// returning a dictionary of supported filter types with appropriate implementation.
        /// <para>
        /// This uses <see cref="IFilterToTypeConverter{TFilter}"/> to parse the filter
        /// value from to client to the given filter type. Most native types are supported.
        /// </para>
        /// </summary>
        /// <typeparam name="TFilter">The filter type.</typeparam>
        /// <param name="propertySelector">The field/property selector.</param>
        /// <param name="builder">The filter factory.</param>
        /// <returns>
        /// Itself. An instance of <see cref="FilterExpressionProviderDictionary{TEntity}"/>.
        /// </returns>
        public FilterExpressionProviderDictionary<TEntity> AddTypedFilter<TFilter>(
            Expression<Func<TEntity, TFilter>> propertySelector,
            Func<TFilter, IDictionary<FilterType, Expression<Func<TEntity, bool>>>> builder) =>
            this.AddTypedFilter(propertySelector.ToPropertyName(), builder);

        private FilterExpressionProviderDictionary<TEntity> AddPropertyFilter(PropertyInfo property)
        {
            if (property.PropertyType == typeof(string))
            {
                this.AddStringFilter(property.Name);
            }
            else if (BooleanTypes.Contains(property.PropertyType))
            {
                this.GetType()
                    .GetRuntimeMethod(nameof(this.AddBooleanFilter), new[] { typeof(string) })
                    .MakeGenericMethod(property.PropertyType)
                    .Invoke(this, new object[] { property.Name });
            }
            else if (DateTimeTypes.Contains(property.PropertyType))
            {
                this.GetType()
                    .GetRuntimeMethod(nameof(this.AddDateTimeFilter), new[] { typeof(string) })
                    .MakeGenericMethod(property.PropertyType)
                    .Invoke(this, new object[] { property.Name });
            }
            else if (property.PropertyType.IsValueType)
            {
                this.GetType()
                    .GetRuntimeMethod(nameof(this.AddNumericFilter), new[] { typeof(string) })
                    .MakeGenericMethod(property.PropertyType)
                    .Invoke(this, new object[] { property.Name });
            }

            return this;
        }
    }
}
