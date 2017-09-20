// <copyright file="ToOkObjectResultAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Microsoft.AspNetCore.Mvc;
    using Operators.ActionResult;
    using Operators.Exceptions;

    public static class ToOkObjectResultAlias
    {
        /// <summary>
        /// Wrap the received value in an <see cref="OkObjectResult"/>.
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToOkObjectResult<TSource>(
            this IProviderObservable<TSource> observable) =>
            observable.ToActionResult(s => new OkObjectResult(s));
    }
}
