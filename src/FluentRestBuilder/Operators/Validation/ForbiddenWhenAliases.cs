﻿// <copyright file="ForbiddenWhenAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Operators.Exceptions;

    public static class ForbiddenWhenAliases
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(invalidCheck, StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            object error) =>
            observable.InvalidWhen(invalidCheck, StatusCodes.Status403Forbidden, s => error);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(
                s => invalidCheck(), StatusCodes.Status403Forbidden, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            object error) =>
            observable.InvalidWhen(
                s => invalidCheck(), StatusCodes.Status403Forbidden, s => error);
    }
}
