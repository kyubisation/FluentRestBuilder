// <copyright file="InvalidWhenOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using Exceptions;

    public static class InvalidWhenOperator
    {
        /// <summary>
        /// Emits an error if the validation check fails. Otherwise emits the value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="statusCode">The status code of the error.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> InvalidWhen<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, bool> invalidCheck,
            int statusCode,
            Func<TSource, object> errorFactory = null) =>
            new InvalidWhenObservable<TSource>(invalidCheck, statusCode, errorFactory, observable);

        private sealed class InvalidWhenObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Func<TSource, bool> invalidCheck;
            private readonly int statusCode;
            private readonly Func<TSource, object> errorFactory;

            public InvalidWhenObservable(
                Func<TSource, bool> invalidCheck,
                int statusCode,
                Func<TSource, object> errorFactory,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(invalidCheck, nameof(invalidCheck));
                this.invalidCheck = invalidCheck;
                this.statusCode = statusCode;
                this.errorFactory = errorFactory;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable) =>
                new InvalidWhenObserver(
                    this.invalidCheck, this.statusCode, this.errorFactory, observer, disposable);

            private sealed class InvalidWhenObserver : SafeObserver
            {
                private readonly Func<TSource, bool> invalidCheck;
                private readonly int statusCode;
                private readonly Func<TSource, object> errorFactory;

                public InvalidWhenObserver(
                    Func<TSource, bool> invalidCheck,
                    int statusCode,
                    Func<TSource, object> errorFactory,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.invalidCheck = invalidCheck;
                    this.statusCode = statusCode;
                    this.errorFactory = errorFactory;
                }

                protected override void SafeOnNext(TSource value)
                {
                    var isInvalid = this.invalidCheck(value);
                    if (isInvalid)
                    {
                        var error = this.errorFactory?.Invoke(value);
                        this.OnError(new ValidationException(this.statusCode, error));
                    }
                    else
                    {
                        this.EmitNext(value);
                    }
                }
            }
        }
    }
}
