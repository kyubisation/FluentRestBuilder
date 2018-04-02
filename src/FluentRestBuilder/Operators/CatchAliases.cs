// <copyright file="CatchAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;

    public static class CatchAliases
    {
        /// <summary>
        /// Catch an exception emitted from the previous observables or operators
        /// and return a new observable.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="handler">The function to handle the exception.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Catch<TSource>(
            this IProviderObservable<TSource> observable,
            Func<Exception, IProviderObservable<TSource>> handler) =>
            observable.Catch<TSource, Exception>(handler);

        /// <summary>
        /// Catch an exception emitted from the previous observables or operators
        /// and perform an action with it. This will only catch the exception if it is
        /// an instance of the declared exception type.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Catch<TSource, TException>(
            this IProviderObservable<TSource> observable,
            Action<TException> action)
            where TException : Exception =>
            observable.Catch((TException exception) =>
            {
                action(exception);
                return Observable.Throw<TSource>(exception, observable.ServiceProvider);
            });

        /// <summary>
        /// Catch an exception emitted from the previous observables or operators
        /// and perform an action with it.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Catch<TSource>(
            this IProviderObservable<TSource> observable,
            Action<Exception> action) =>
            observable.Catch<TSource, Exception>(action);
    }
}
