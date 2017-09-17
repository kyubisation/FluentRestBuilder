// <copyright file="ToActionResultOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ActionResult
{
    using System;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            new ToActionResultObservable<TSource>(mapping, observable);

        private sealed class ToActionResultObservable<TSource> : Operator<TSource, IActionResult>
        {
            private readonly Func<TSource, IActionResult> mapping;

            public ToActionResultObservable(
                Func<TSource, IActionResult> mapping, IProviderObservable<TSource> observable)
                : base(observable)
            {
                this.mapping = mapping;
            }

            protected override IObserver<TSource> Create(
                IObserver<IActionResult> observer, IDisposable disposable) =>
                new ToActionResultObserver(this.mapping, observer, disposable);

            private sealed class ToActionResultObserver : SafeObserver
            {
                private readonly Func<TSource, IActionResult> mapping;

                public ToActionResultObserver(
                    Func<TSource, IActionResult> mapping,
                    IObserver<IActionResult> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                }

                public override void OnError(Exception error)
                {
                    if (error is ValidationException validationException)
                    {
                        var actionResult = ToActionResult(validationException);
                        this.EmitNext(actionResult);
                        this.OnCompleted();
                    }
                    else
                    {
                        base.OnError(error);
                    }
                }

                protected override void SafeOnNext(TSource value)
                {
                    var actionResult = this.mapping(value);
                    this.EmitNext(actionResult);
                }

                private static IActionResult ToActionResult(ValidationException exception)
                {
                    return exception.Error == null
                        ? ToStatusResult(exception) : ToObjectResult(exception);
                }

                private static IActionResult ToObjectResult(ValidationException exception)
                {
                    switch (exception.StatusCode)
                    {
                        case StatusCodes.Status400BadRequest:
                            return new BadRequestObjectResult(exception.Error);
                        case StatusCodes.Status404NotFound:
                            return new NotFoundObjectResult(exception.Error);
                        default:
                            return new ObjectResult(exception.Error)
                            {
                                StatusCode = exception.StatusCode,
                            };
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
    }
}
