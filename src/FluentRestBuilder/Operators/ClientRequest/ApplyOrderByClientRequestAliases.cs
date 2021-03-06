﻿// <copyright file="ApplyOrderByClientRequestAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Operators.ClientRequest.Interpreters;
    using Operators.ClientRequest.OrderByExpressions;

    public static class ApplyOrderByClientRequestAliases
    {
        /// <summary>
        /// Apply order by logic to the received <see cref="IQueryable{T}"/>.
        /// <para>
        /// The default query parameter key is "sort".
        /// A comma-separated list of properties is supported.
        /// Prefix the property with "-" to sort descending.
        /// Implement <see cref="IOrderByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// <para>Requires <see cref="HttpContextProviderAttribute"/> to be set.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="factory">
        /// A factory function to create a dictionary of supported order by expressions.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Func<OrderByExpressionDictionary<TSource>, IOrderByExpressionDictionary<TSource>> factory)
        {
            var dictionary = factory(new OrderByExpressionDictionary<TSource>());
            return observable.ApplyOrderByClientRequest(dictionary);
        }

        /// <summary>
        /// Apply order by logic to the received <see cref="IQueryable{T}"/>.
        /// Tries to resolve <see cref="IOrderByExpressionDictionary{TSource}"/>
        /// via <see cref="IServiceProvider"/>.
        /// <para>
        /// The default query parameter key is "sort".
        /// A comma-separated list of properties is supported.
        /// Prefix the property with "-" to sort descending.
        /// Implement <see cref="IOrderByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// <para>Requires <see cref="HttpContextProviderAttribute"/> to be set.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable)
        {
            var dictionary = observable.ServiceProvider
                .GetRequiredService<IOrderByExpressionDictionary<TSource>>();
            return observable.ApplyOrderByClientRequest(dictionary);
        }
    }
}
