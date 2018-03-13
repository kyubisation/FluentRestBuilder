// <copyright file="ToActionResultOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Operators.Exceptions;

    public static class ToActionResultOperator
    {
        /// <summary>
        /// Convert the received value into an <see cref="IActionResult"/>.
        /// <para>
        /// Catches <see cref="ValidationException"/> and converts it to
        /// an appropriate <see cref="IActionResult"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">
        /// The function to convert the value into an <see cref="IActionResult"/>.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{IActionResult}"/>.</returns>
        public static IProviderObservable<IActionResult> ToActionResult<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, IActionResult> mapping) =>
            observable.Map(mapping)
                .Catch((ValidationException exception) =>
                {
                    var actionResult = exception.Error == null
                        ? ToStatusResult(exception) : ToObjectResult(exception);
                    return Observable.Single(actionResult, observable.ServiceProvider);
                });

        private static IActionResult ToObjectResult(ValidationException exception)
        {
            switch (exception.StatusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestObjectResult(exception.Error);
                case StatusCodes.Status404NotFound:
                    return new NotFoundObjectResult(exception.Error);
                default:
                    return new ObjectResult(exception.Error) { StatusCode = exception.StatusCode };
            }
        }

        private static IActionResult ToStatusResult(ValidationException exception)
        {
            switch (exception.StatusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestResult();
                case StatusCodes.Status404NotFound:
                    return new NotFoundResult();
                default:
                    return new StatusCodeResult(exception.StatusCode);
            }
        }
    }
}
