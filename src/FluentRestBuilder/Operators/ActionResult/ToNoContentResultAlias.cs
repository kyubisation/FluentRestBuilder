// <copyright file="ToNoContentResultAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Microsoft.AspNetCore.Mvc;
    using Operators.Exceptions;

    public static class ToNoContentResultAlias
    {
        /// <summary>
        /// Emits <see cref="NoContentResult"/> on receiving a value. Does not contain the value.
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToNoContentResult<TSource>(
            this IProviderObservable<TSource> observable) =>
            observable.ToActionResult(s => new NoContentResult());
    }
}
