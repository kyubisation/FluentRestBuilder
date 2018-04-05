// <copyright file="Observable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Observables;

    public static class Observable
    {
        /// <summary>
        /// Create an observable with the given value.
        /// If no service provider is given, an empty service provider will be used.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="value">The value to be used.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Single<TSource>(
            TSource value, IServiceProvider serviceProvider = null) =>
            new SingleObservable<TSource>(value, serviceProvider ?? EmptyProvider());

        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the awaited value.
        /// If no service provider is given, an empty service provider will be used.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="valueFactory">The value factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> AsyncSingle<TSource>(
            Lazy<TSource> valueFactory, IServiceProvider serviceProvider = null) =>
            new AsyncSingleObservable<TSource>(valueFactory, serviceProvider ?? EmptyProvider());

        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the awaited value.
        /// If no service provider is given, an empty service provider will be used.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="valueFactory">The value factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> AsyncSingle<TSource>(
            Func<TSource> valueFactory, IServiceProvider serviceProvider = null) =>
            new AsyncSingleObservable<TSource>(valueFactory, serviceProvider ?? EmptyProvider());

        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the awaited value.
        /// If no service provider is given, an empty service provider will be used.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="valueFactory">The value factory.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> AsyncSingle<TSource>(
            Func<Task<TSource>> valueFactory, IServiceProvider serviceProvider = null) =>
            new AsyncSingleObservable<TSource>(valueFactory, serviceProvider ?? EmptyProvider());

        /// <summary>
        /// Create an observable with the given exception.
        /// Will emit the provided exception immediately as an error upon subscription.
        /// </summary>
        /// <typeparam name="TSource">The type of the expected value.</typeparam>
        /// <param name="exception">The error.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Throw<TSource>(
            Exception exception, IServiceProvider serviceProvider = null) =>
            new ErrorObservable<TSource>(exception, serviceProvider ?? EmptyProvider());

        private static IServiceProvider EmptyProvider() =>
            new ServiceCollection().BuildServiceProvider();
    }
}
