// <copyright file="InvalidWhenAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Operators.Exceptions;

    public static class InvalidWhenAliases
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the given status code.
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="statusCode">The status code of the error.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> InvalidWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            int statusCode,
            object error) =>
            observable.InvalidWhen(invalidCheck, statusCode, s => error);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the given status code.
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="statusCode">The status code of the error.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> InvalidWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            int statusCode,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(s => invalidCheck(), statusCode, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the given status code.
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="statusCode">The status code of the error.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> InvalidWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            int statusCode,
            object error) =>
            observable.InvalidWhen(s => invalidCheck(), statusCode, s => error);
    }
}
