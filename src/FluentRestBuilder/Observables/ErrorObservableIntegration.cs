// <copyright file="ErrorObservableIntegration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    public static class ErrorObservableIntegration
    {
        /// <summary>
        /// Create an observable which emits the given exception.
        /// </summary>
        /// <typeparam name="TSource">The type of the given value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="error">The error to emit.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> ThrowObservable<TSource>(
            this ControllerBase controller, Exception error)
        {
            Check.IsNull(controller, nameof(controller));
            return Observable.Throw<TSource>(error, controller.HttpContext.RequestServices);
        }
    }
}
