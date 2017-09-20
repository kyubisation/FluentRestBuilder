// <copyright file="CurrentUserHasAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Security.Claims;
    using Filters;
    using Operators.Exceptions;

    public static class CurrentUserHasAliases
    {
        /// <summary>
        /// If the check returns <c>false</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="principalCheck">The principal check function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHas<TSource>(
            this IProviderObservable<TSource> observable,
            Func<ClaimsPrincipal, TSource, bool> principalCheck,
            object error) =>
            observable.CurrentUserHas(principalCheck, s => error);

        /// <summary>
        /// If the check returns <c>false</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="principalCheck">The principal check function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHas<TSource>(
            this IProviderObservable<TSource> observable,
            Func<ClaimsPrincipal, bool> principalCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.CurrentUserHas((p, s) => principalCheck(p), errorFactory);

        /// <summary>
        /// If the check returns <c>false</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="principalCheck">The principal check function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHas<TSource>(
            this IProviderObservable<TSource> observable,
            Func<ClaimsPrincipal, bool> principalCheck,
            object error) =>
            observable.CurrentUserHas((p, s) => principalCheck(p), s => error);
    }
}
