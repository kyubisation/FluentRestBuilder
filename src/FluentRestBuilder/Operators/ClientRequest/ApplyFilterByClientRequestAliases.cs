// <copyright file="ApplyFilterByClientRequestAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Operators.ClientRequest.FilterExpressions;
    using Operators.ClientRequest.Interpreters;

    public static class ApplyFilterByClientRequestAliases
    {
        /// <summary>
        /// Apply filter logic to the received <see cref="IQueryable{T}"/>.
        /// <para>
        /// Matches the query parameters with the keys of the given filter dictionary.
        /// Implement <see cref="IFilterByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// <para>Requires <see cref="HttpContextProviderAttribute"/> to be set.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="factory">
        /// A factory function to create a dictionary of supported filter expressions.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Func<FilterExpressionProviderDictionary<TSource>, IFilterExpressionProviderDictionary<TSource>> factory)
        {
            var filterProvider = new FilterExpressionProviderDictionary<TSource>(
                observable.ServiceProvider);
            var filterDictionary = factory(filterProvider);
            return observable.ApplyFilterByClientRequest(filterDictionary);
        }

        /// <summary>
        /// Apply filter logic to the received <see cref="IQueryable{T}"/>.
        /// Tries to resolve <see cref="IFilterExpressionProviderDictionary{TEntity}"/>
        /// via <see cref="IServiceProvider"/>.
        /// <para>
        /// Matches the query parameters with the keys of the given filter dictionary.
        /// Implement <see cref="IFilterByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// <para>Requires <see cref="HttpContextProviderAttribute"/> to be set.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable)
        {
            var filterDictionary = observable.ServiceProvider
                .GetService<IFilterExpressionProviderDictionary<TSource>>();
            return observable.ApplyFilterByClientRequest(filterDictionary);
        }
    }
}
