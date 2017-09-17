// <copyright file="ToOptionsResultAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Operators.ActionResult;
    using Operators.ActionResult.Options;
    using Operators.Exceptions;
    using Storage;

    public static class ToOptionsResultAliases
    {
        /// <summary>
        /// Emits an <see cref="OptionsResult"/> which lists the allowed
        /// HTTP verbs.
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="verbsFactory">A factory function for the allowed HTTP verbs.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToOptionsResult<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, IEnumerable<HttpVerb>> verbsFactory) =>
            observable.ToActionResult(s => new OptionsResult(verbsFactory(s)));

        /// <summary>
        /// Emits an <see cref="OptionsResult"/> which lists the allowed
        /// HTTP verbs.
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="factory">A factory function for the allowed HTTP verbs.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToOptionsResult<TSource>(
            this IProviderObservable<TSource> observable,
            Func<AllowedOptionsBuilder<TSource>, AllowedOptionsBuilder<TSource>> factory)
        {
            Check.IsNull(factory, nameof(factory));
            return observable.ToOptionsResult(s =>
            {
                var httpContext = observable.ServiceProvider
                    .GetRequiredService<IScopedStorage<HttpContext>>();
                var allowedOptionsBuilder = new AllowedOptionsBuilder<TSource>(
                    httpContext?.Value.User);
                return factory(allowedOptionsBuilder)
                    .GenerateAllowedVerbs(s);
            });
        }
    }
}
