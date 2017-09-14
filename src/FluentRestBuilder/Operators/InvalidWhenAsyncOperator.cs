// <copyright file="InvalidWhenAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Threading.Tasks;
    using Exceptions;

    public static class InvalidWhenAsyncOperator
    {
        /// <summary>
        /// If the check returns <c>true</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the given status code.
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="invalidCheck">The invalidCheck function.</param>
        /// <param name="statusCode">The status code of the error.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> InvalidWhenAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task<bool>> invalidCheck,
            int statusCode,
            Func<TSource, object> errorFactory = null) =>
            new InvalidWhenAsyncObservable<TSource>(invalidCheck, statusCode, errorFactory, observable);

        private sealed class InvalidWhenAsyncObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Func<TSource, Task<bool>> invalidCheck;
            private readonly int statusCode;
            private readonly Func<TSource, object> errorFactory;

            public InvalidWhenAsyncObservable(
                Func<TSource, Task<bool>> invalidCheck,
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
                new InvalidWhenAsyncObserver(
                    this.invalidCheck, this.statusCode, this.errorFactory, observer, disposable);

            private sealed class InvalidWhenAsyncObserver : SafeAsyncObserver
            {
                private readonly Func<TSource, Task<bool>> invalidCheck;
                private readonly int statusCode;
                private readonly Func<TSource, object> errorFactory;

                public InvalidWhenAsyncObserver(
                    Func<TSource, Task<bool>> invalidCheck,
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

                protected override async Task SafeOnNext(TSource value)
                {
                    var isInvalid = await this.invalidCheck(value);
                    if (isInvalid)
                    {
                        var error = this.errorFactory?.Invoke(value);
                        this.EmitError(new ValidationException(this.statusCode, error));
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
