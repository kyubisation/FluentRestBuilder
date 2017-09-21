// <copyright file="ApplySearchByClientRequestOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.ClientRequest.Interpreters;
    using Storage;

    public static class ApplySearchByClientRequestOperator
    {
        /// <summary>
        /// Apply a global search to the received <see cref="IQueryable{T}"/>.
        /// <para>
        /// The default query parameter key is "q".
        /// Implement <see cref="ISearchByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="searchExpression">The global search expression factory function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplySearchByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Func<string, Expression<Func<TSource, bool>>> searchExpression) =>
            new ApplySearchByClientRequestObservable<TSource>(searchExpression, observable);

        private sealed class ApplySearchByClientRequestObservable<TSource>
            : Operator<IQueryable<TSource>, IQueryable<TSource>>
        {
            private readonly Func<string, Expression<Func<TSource, bool>>> searchExpression;

            public ApplySearchByClientRequestObservable(
                Func<string, Expression<Func<TSource, bool>>> searchExpression,
                IProviderObservable<IQueryable<TSource>> observable)
                : base(observable)
            {
                this.searchExpression = searchExpression;
            }

            protected override IObserver<IQueryable<TSource>> Create(
                IObserver<IQueryable<TSource>> observer, IDisposable disposable)
            {
                var interpreter = this.ServiceProvider
                    .GetService<ISearchByClientRequestInterpreter>()
                    ?? this.CreateDefaultInterpreter();
                return new ApplySearchByClientRequestObserver(
                    this.searchExpression, interpreter, observer, disposable);
            }

            private ISearchByClientRequestInterpreter CreateDefaultInterpreter()
            {
                var httpContextStorage = this.ServiceProvider
                    .GetService<IScopedStorage<HttpContext>>();
                return new SearchByClientRequestInterpreter(httpContextStorage);
            }

            private sealed class ApplySearchByClientRequestObserver : SafeObserver
            {
                private readonly Func<string, Expression<Func<TSource, bool>>> searchExpression;
                private readonly ISearchByClientRequestInterpreter interpreter;

                public ApplySearchByClientRequestObserver(
                    Func<string, Expression<Func<TSource, bool>>> searchExpression,
                    ISearchByClientRequestInterpreter interpreter,
                    IObserver<IQueryable<TSource>> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.searchExpression = searchExpression;
                    this.interpreter = interpreter;
                }

                protected override void SafeOnNext(IQueryable<TSource> value)
                {
                    var searchString = this.interpreter.ParseRequestQuery();
                    if (string.IsNullOrEmpty(searchString))
                    {
                        this.EmitNext(value);
                        return;
                    }

                    var expression = this.searchExpression(searchString);
                    this.EmitNext(value.Where(expression));
                }
            }
        }
    }
}
