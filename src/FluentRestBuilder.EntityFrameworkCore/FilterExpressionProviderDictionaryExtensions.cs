// <copyright file="FilterExpressionProviderDictionaryExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Operators.ClientRequest.FilterExpressions;

    public static class FilterExpressionProviderDictionaryExtensions
    {
        /// <summary>
        /// Adds equals and contains filter for the given string field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="propertySelector">The field/property selection.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TEntity}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddEqualAndContainsStringFilter<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            Expression<Func<TInput, string>> propertySelector)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddEqualAndContainsStringFilter(member.Member.Name);
        }

        /// <summary>
        /// Adds equals and contains filter for the given string field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddEqualAndContainsStringFilter<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary, string property) =>
            dictionary.AddFilter(
                property,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<string>(e, property) == f)
                    .AddContains(e => EF.Property<string>(e, property).Contains(f)));

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given integer field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="propertySelector">The field/property selection.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddIntegerFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            Expression<Func<TInput, int>> propertySelector)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddIntegerFilters(member.Member.Name);
        }

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given integer field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddIntegerFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            string property) =>
            dictionary.AddTypedFilter<int>(
                property,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<int>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<int>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<int>(e, property) >= f)
                    .AddLessThan(e => EF.Property<int>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<int>(e, property) <= f));

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given double field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="propertySelector">The field/property selection.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddDoubleFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            Expression<Func<TInput, double>> propertySelector)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddDoubleFilters(member.Member.Name);
        }

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given double field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddDoubleFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            string property) =>
            dictionary.AddTypedFilter<double>(
                property,
                (f, expressions) => expressions
                    //// ReSharper disable once CompareOfFloatsByEqualityOperator
                    .AddEquals(e => EF.Property<double>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<double>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<double>(e, property) >= f)
                    .AddLessThan(e => EF.Property<double>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<double>(e, property) <= f));

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given <see cref="DateTime"/> field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="propertySelector">The field/property selection.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddDateTimeFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            Expression<Func<TInput, DateTime>> propertySelector)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddDateTimeFilters(member.Member.Name);
        }

        /// <summary>
        /// Adds equals, greater than, greater than or equal, less than, less than or equal
        /// filter for the given <see cref="DateTime"/> field/property.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="dictionary">The expression provider dictionary.</param>
        /// <param name="property">The field/property name.</param>
        /// <returns>
        /// Itself, an <see cref="FilterExpressionProviderDictionary{TInput}"/> instance.
        /// </returns>
        public static FilterExpressionProviderDictionary<TInput> AddDateTimeFilters<TInput>(
            this FilterExpressionProviderDictionary<TInput> dictionary,
            string property) =>
            dictionary.AddTypedFilter<DateTime>(
                property,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<DateTime>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<DateTime>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<DateTime>(e, property) >= f)
                    .AddLessThan(e => EF.Property<DateTime>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<DateTime>(e, property) <= f));
    }
}
