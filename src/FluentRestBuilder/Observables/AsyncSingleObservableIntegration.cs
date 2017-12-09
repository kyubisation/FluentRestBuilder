// <copyright file="AsyncSingleObservableIntegration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public static class AsyncSingleObservableIntegration
    {
        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the awaited value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
            this ControllerBase controller, Func<Task<TSource>> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return Observable.AsyncSingle(valueFactory, controller.HttpContext.RequestServices);
        }

        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
            this ControllerBase controller, Func<TSource> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return Observable.AsyncSingle(valueFactory, controller.HttpContext.RequestServices);
        }

        /// <summary>
        /// Create an observable which uses provided <see cref="Lazy{T}"/>
        /// value on subscription and emits it.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
            this ControllerBase controller, Lazy<TSource> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return Observable.AsyncSingle(valueFactory, controller.HttpContext.RequestServices);
        }
    }
}
