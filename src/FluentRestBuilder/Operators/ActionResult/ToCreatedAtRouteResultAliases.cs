// <copyright file="ToCreatedAtRouteResultAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Operators.Exceptions;

    public static class ToCreatedAtRouteResultAliases
    {
        /// <summary>
        /// Wrap the received value in an <see cref="CreatedAtRouteResult"/>
        /// with status code 201 (Created).
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="routeName">The name of the route to use for generating the URL.</param>
        /// <param name="routeValuesFactory">
        /// The factory function for the route data to use for generating the URL.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToCreatedAtRouteResult<TSource>(
            this IProviderObservable<TSource> observable,
            string routeName,
            Func<TSource, object> routeValuesFactory) =>
            observable.ToActionResult(
                s => new CreatedAtRouteResult(routeName, routeValuesFactory(s), s));

        /// <summary>
        /// Wrap the received value in an <see cref="CreatedAtRouteResult"/>
        /// with status code 201 (Created).
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="routeValuesFactory">
        /// The factory function for the route data to use for generating the URL.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToCreatedAtRouteResult<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, object> routeValuesFactory) =>
            observable.ToActionResult(s => new CreatedAtRouteResult(routeValuesFactory(s), s));
    }
}
