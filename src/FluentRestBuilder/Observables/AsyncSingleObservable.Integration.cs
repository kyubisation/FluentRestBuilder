// <copyright file="AsyncSingleObservable.Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Observables;

    public static partial class Integration
    {
        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the awaited value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="AsyncSingleObservable{T}"/>.</returns>
        public static AsyncSingleObservable<T> CreateAsyncSingle<T>(
            this ControllerBase controller, Func<Task<T>> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return new AsyncSingleObservable<T>(
                valueFactory, controller.HttpContext.RequestServices);
        }

        /// <summary>
        /// Create an observable which calls the provided factory
        /// function on subscription and emits the resulting value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="AsyncSingleObservable{T}"/>.</returns>
        public static AsyncSingleObservable<T> CreateAsyncSingle<T>(
            this ControllerBase controller, Func<T> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return new AsyncSingleObservable<T>(
                valueFactory, controller.HttpContext.RequestServices);
        }

        /// <summary>
        /// Create an observable which uses provided <see cref="Lazy{T}"/>
        /// value on subscription and emits it.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <returns>An instance of <see cref="AsyncSingleObservable{T}"/>.</returns>
        public static AsyncSingleObservable<T> CreateAsyncSingle<T>(
            this ControllerBase controller, Lazy<T> valueFactory)
        {
            Check.IsNull(controller, nameof(controller));
            return new AsyncSingleObservable<T>(
                valueFactory, controller.HttpContext.RequestServices);
        }
    }
}
