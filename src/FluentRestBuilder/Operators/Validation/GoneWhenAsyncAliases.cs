// <copyright file="GoneWhenAsyncAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Operators;
    using Operators.Exceptions;

    public static class GoneWhenAsyncAliases
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 410 (Gone).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> GoneWhenAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task<bool>> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhenAsync(invalidCheck, StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 410 (Gone).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> GoneWhenAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task<bool>> invalidCheck,
            object error) =>
            observable.InvalidWhenAsync(invalidCheck, StatusCodes.Status410Gone, s => error);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 410 (Gone).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> GoneWhenAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<Task<bool>> invalidCheck,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhenAsync(
                s => invalidCheck(), StatusCodes.Status410Gone, errorFactory);

        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 410 (Gone).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="error">The error to be used on a failed check.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> GoneWhenAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<Task<bool>> invalidCheck,
            object error) =>
            observable.InvalidWhenAsync(
                s => invalidCheck(), StatusCodes.Status410Gone, s => error);
    }
}
