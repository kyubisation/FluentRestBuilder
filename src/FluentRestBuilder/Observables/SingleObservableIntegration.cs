// <copyright file="SingleObservableIntegration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Microsoft.AspNetCore.Mvc;

    public static class SingleObservableIntegration
    {
        /// <summary>
        /// Create an observable which emits the given value.
        /// </summary>
        /// <typeparam name="TSource">The type of the given value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="value">The value.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateSingle<TSource>(
            this ControllerBase controller, TSource value)
        {
            Check.IsNull(controller, nameof(controller));
            return Observable.Single(value, controller.HttpContext.RequestServices);
        }
    }
}
