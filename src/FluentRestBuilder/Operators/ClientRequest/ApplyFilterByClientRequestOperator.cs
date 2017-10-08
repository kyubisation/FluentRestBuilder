// <copyright file="ApplyFilterByClientRequestOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.ClientRequest.FilterExpressions;
    using Operators.ClientRequest.Interpreters;
    using Storage;

    public static class ApplyFilterByClientRequestOperator
    {
        /// <summary>
        /// Apply filter logic to the received <see cref="IQueryable{T}"/>.
        /// <para>
        /// Matches the query parameters with the keys of the given filter dictionary.
        /// Implement <see cref="IFilterByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="filterDictionary">A dictionary of available filters.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            IDictionary<string, IFilterExpressionProvider<TSource>> filterDictionary) =>
            new ApplyFilterByClientRequestObservable<TSource>(filterDictionary, observable);

        private sealed class ApplyFilterByClientRequestObservable<TSource> : Operator<IQueryable<TSource>, IQueryable<TSource>>
        {
            private readonly IDictionary<string, IFilterExpressionProvider<TSource>> filterDictionary;

            public ApplyFilterByClientRequestObservable(
                IDictionary<string, IFilterExpressionProvider<TSource>> filterDictionary,
                IProviderObservable<IQueryable<TSource>> observable)
                : base(observable)
            {
                this.filterDictionary = filterDictionary;
            }

            protected override IObserver<IQueryable<TSource>> Create(
                IObserver<IQueryable<TSource>> observer, IDisposable disposable)
            {
                var interpreter = this.ServiceProvider
                    .GetService<IFilterByClientRequestInterpreter>()
                    ?? this.CreateDefaultInterpreter();
                return new ApplyFilterByClientRequestObserver(
                    this.filterDictionary, interpreter, observer, disposable);
            }

            private IFilterByClientRequestInterpreter CreateDefaultInterpreter()
            {
                var httpContextStorage = this.ServiceProvider
                    .GetService<IScopedStorage<HttpContext>>();
                return new FilterByClientRequestInterpreter(httpContextStorage);
            }

            private sealed class ApplyFilterByClientRequestObserver : SafeObserver
            {
                private readonly IDictionary<string, IFilterExpressionProvider<TSource>> filterDictionary;
                private readonly IFilterByClientRequestInterpreter interpreter;

                public ApplyFilterByClientRequestObserver(
                    IDictionary<string, IFilterExpressionProvider<TSource>> filterDictionary,
                    IFilterByClientRequestInterpreter interpreter,
                    IObserver<IQueryable<TSource>> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.filterDictionary = filterDictionary;
                    this.interpreter = interpreter;
                }

                protected override IQueryable<TSource> SafeOnNext(IQueryable<TSource> value)
                {
                    var filterRequests = this.interpreter.ParseRequestQuery(this.filterDictionary.Keys);
                    return this.ApplyFilters(filterRequests, value);
                }

                private IQueryable<TSource> ApplyFilters(
                    IEnumerable<FilterRequest> filterRequests, IQueryable<TSource> queryable) =>
                    filterRequests
                        .Select(this.ResolveFilterExpression)
                        .Where(f => f != null)
                        .Aggregate(queryable, (current, next) => current.Where(next));

                private Expression<Func<TSource, bool>> ResolveFilterExpression(FilterRequest request)
                {
                    return this.filterDictionary[request.Property].Resolve(request.FilterType, request.Filter);
                }
            }
        }
    }
}
