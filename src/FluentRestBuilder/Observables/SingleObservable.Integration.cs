// <copyright file="SingleObservable.Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Microsoft.AspNetCore.Mvc;
    using Observables;

    public static partial class Integration
    {
        /// <summary>
        /// Create an observable which emits the given value.
        /// </summary>
        /// <typeparam name="T">The type of the given value.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="value">The value.</param>
        /// <returns>An instance of <see cref="SingleObservable{T}"/>.</returns>
        public static SingleObservable<T> CreateSingle<T>(
            this ControllerBase controller, T value)
        {
            Check.IsNull(controller, nameof(controller));
            return new SingleObservable<T>(value, controller.HttpContext.RequestServices);
        }
    }
}
