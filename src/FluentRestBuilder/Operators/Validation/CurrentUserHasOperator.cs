// <copyright file="CurrentUserHasOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Security.Claims;
    using Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.Exceptions;
    using Storage;

    public static class CurrentUserHasOperator
    {
        /// <summary>
        /// If the check returns <c>false</c>, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 403 (Forbidden).
        /// Otherwise the given value is emitted.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="principalCheck">The principal check function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> CurrentUserHas<TSource>(
            this IProviderObservable<TSource> observable,
            Func<ClaimsPrincipal, TSource, bool> principalCheck,
            Func<TSource, object> errorFactory = null) =>
            new CurrentUserHasObservable<TSource>(principalCheck, errorFactory, observable);

        private sealed class CurrentUserHasObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Func<ClaimsPrincipal, TSource, bool> principalCheck;
            private readonly Func<TSource, object> errorFactory;

            public CurrentUserHasObservable(
                Func<ClaimsPrincipal, TSource, bool> principalCheck,
                Func<TSource, object> errorFactory,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(principalCheck, nameof(principalCheck));
                this.principalCheck = principalCheck;
                this.errorFactory = errorFactory;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable)
            {
                var httpContext = this.ServiceProvider.GetService<IScopedStorage<HttpContext>>();
                return new CurrentUserHasObserver(
                    httpContext.Value?.User,
                    this.principalCheck,
                    this.errorFactory,
                    observer,
                    disposable);
            }

            private sealed class CurrentUserHasObserver : SafeObserver
            {
                private readonly ClaimsPrincipal principal;
                private readonly Func<ClaimsPrincipal, TSource, bool> principalCheck;
                private readonly Func<TSource, object> errorFactory;

                public CurrentUserHasObserver(
                    ClaimsPrincipal principal,
                    Func<ClaimsPrincipal, TSource, bool> principalCheck,
                    Func<TSource, object> errorFactory,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    Check.IsNull(principal, nameof(principal));
                    this.principal = principal;
                    this.principalCheck = principalCheck;
                    this.errorFactory = errorFactory;
                }

                protected override TSource SafeOnNext(TSource value)
                {
                    if (this.principalCheck(this.principal, value))
                    {
                        return value;
                    }

                    var error = this.errorFactory?.Invoke(value);
                    this.OnError(new ValidationException(StatusCodes.Status403Forbidden, error));
                    return default(TSource);
                }
            }
        }
    }
}
