// <copyright file="PrincipalHasAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Exceptions;
    using Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public static class PrincipalHasAsyncOperator
    {
        /// <summary>
        /// Emits an error if the asynchronous principal (the current user) check fails.
        /// Otherwise emits the value.
        /// <para>Requires usage of <see cref="HttpContextProviderAttribute"/>.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="principalCheck">The principal check function.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> PrincipalHasAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<ClaimsPrincipal, TSource, Task<bool>> principalCheck,
            Func<TSource, object> errorFactory = null) =>
            new PrincipalHasAsyncObservable<TSource>(principalCheck, errorFactory, observable);

        private sealed class PrincipalHasAsyncObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Func<ClaimsPrincipal, TSource, Task<bool>> principalCheck;
            private readonly Func<TSource, object> errorFactory;

            public PrincipalHasAsyncObservable(
                Func<ClaimsPrincipal, TSource, Task<bool>> principalCheck,
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
                return new PrincipalHasAsyncObserver(
                    httpContext.Value?.User,
                    this.principalCheck,
                    this.errorFactory,
                    observer,
                    disposable);
            }

            private sealed class PrincipalHasAsyncObserver : SafeAsyncObserver
            {
                private readonly ClaimsPrincipal principal;
                private readonly Func<ClaimsPrincipal, TSource, Task<bool>> principalCheck;
                private readonly Func<TSource, object> errorFactory;

                public PrincipalHasAsyncObserver(
                    ClaimsPrincipal principal,
                    Func<ClaimsPrincipal, TSource, Task<bool>> principalCheck,
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

                protected override async Task SafeOnNext(TSource value)
                {
                    var isValid = await this.principalCheck(this.principal, value);
                    if (isValid)
                    {
                        this.EmitNext(value);
                    }
                    else
                    {
                        var error = this.errorFactory?.Invoke(value);
                        this.OnError(
                            new ValidationException(StatusCodes.Status403Forbidden, error));
                    }
                }
            }
        }
    }
}
