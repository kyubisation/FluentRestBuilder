﻿// <copyright file="BadRequestWhenAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Operators.Exceptions;

    public static class BadRequestWhenAliases
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 400 (Bad Request).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> BadRequestWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(invalidCheck, StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 400 (Bad Request).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> BadRequestWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            object error) =>
            observable.InvalidWhen(invalidCheck, StatusCodes.Status400BadRequest, s => error);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 400 (Bad Request).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> BadRequestWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(
                s => invalidCheck(), StatusCodes.Status400BadRequest, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 400 (Bad Request).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> BadRequestWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<bool> invalidCheck,
            object error) =>
            observable.InvalidWhen(
                s => invalidCheck(), StatusCodes.Status400BadRequest, s => error);
    }
}
