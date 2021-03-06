﻿// <copyright file="ApplyOrderByClientRequestOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.ClientRequest.Interpreters;
    using Operators.ClientRequest.Interpreters.Requests;
    using Operators.ClientRequest.OrderByExpressions;

    public static class ApplyOrderByClientRequestOperator
    {
        /// <summary>
        /// Apply order by logic to the received <see cref="IQueryable{T}"/>.
        /// Provide a dictionary with provided order by expressions.
        /// <para>
        /// The default query parameter key is "sort".
        /// A comma-separated list of properties is supported.
        /// Prefix the property with "-" to sort descending.
        /// Implement <see cref="IOrderByClientRequestInterpreter"/> for custom behavior.
        /// </para>
        /// <para>Requires <see cref="HttpContextProviderAttribute"/> to be set.</para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="orderByExpressions">
        /// A dictionary of supported order by expressions.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            IDictionary<string, IOrderByExpressionFactory<TSource>> orderByExpressions) =>
            new ApplyOrderByClientRequestObservable<TSource>(orderByExpressions, observable);

        private sealed class ApplyOrderByClientRequestObservable<TSource>
            : Operator<IQueryable<TSource>, IQueryable<TSource>>
        {
            private readonly IDictionary<string, IOrderByExpressionFactory<TSource>> orderByExpressionsDictionary;

            public ApplyOrderByClientRequestObservable(
                IDictionary<string, IOrderByExpressionFactory<TSource>> orderByExpressionsDictionary,
                IProviderObservable<IQueryable<TSource>> observable)
                : base(observable)
            {
                this.orderByExpressionsDictionary = orderByExpressionsDictionary;
            }

            protected override IObserver<IQueryable<TSource>> Create(
                IObserver<IQueryable<TSource>> observer, IDisposable disposable)
            {
                var interpreter = this.ServiceProvider
                    .GetService<IOrderByClientRequestInterpreter>();
                return new ApplyOrderByClientRequestObserver(
                    this.orderByExpressionsDictionary, interpreter, observer, disposable);
            }

            private sealed class ApplyOrderByClientRequestObserver : SafeObserver
            {
                private readonly IDictionary<string, IOrderByExpressionFactory<TSource>> orderByExpressionsDictionary;
                private readonly IOrderByClientRequestInterpreter interpreter;

                public ApplyOrderByClientRequestObserver(
                    IDictionary<string, IOrderByExpressionFactory<TSource>> orderByExpressionsDictionary,
                    IOrderByClientRequestInterpreter interpreter,
                    IObserver<IQueryable<TSource>> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.orderByExpressionsDictionary = orderByExpressionsDictionary;
                    this.interpreter = interpreter;
                }

                protected override IQueryable<TSource> SafeOnNext(IQueryable<TSource> value)
                {
                    var orderByExpressions = this.ResolveOrderBySequence();
                    return orderByExpressions.Count == 0
                        ? value : this.ApplyOrderBy(orderByExpressions, value);
                }

                private List<IOrderByExpression<TSource>> ResolveOrderBySequence() =>
                    this.interpreter
                        .ParseRequestQuery(this.orderByExpressionsDictionary.Keys)
                        .Select(this.CreateExpression)
                        .ToList();

                private IOrderByExpression<TSource> CreateExpression(OrderByRequest request)
                {
                    return this.orderByExpressionsDictionary[request.Property]
                        .Create(request.Direction);
                }

                private IQueryable<TSource> ApplyOrderBy(
                    List<IOrderByExpression<TSource>> orderByExpressions, IQueryable<TSource> queryable)
                {
                    var firstOrderByValue = orderByExpressions.First();
                    return orderByExpressions
                        .Skip(1)
                        .Aggregate(
                            firstOrderByValue.OrderBy(queryable),
                            (current, orderBy) => orderBy.ThenBy(current));
                }
            }
        }
    }
}
