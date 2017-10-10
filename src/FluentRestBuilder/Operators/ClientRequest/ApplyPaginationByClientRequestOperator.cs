// <copyright file="ApplyPaginationByClientRequestOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.ClientRequest;
    using Operators.ClientRequest.Interpreters;
    using Storage;

    public static class ApplyPaginationByClientRequestOperator
    {
        /// <summary>
        /// Configure the pagination capabilities.
        /// <para>
        /// WARNING: Do not use this before FilterByClientRequest, SearchByClientRequest or
        /// OrderByClientRequest! This would result in erroneous pagination logic.
        /// </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="options">The pagination options.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> ApplyPaginationByClientRequest<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            PaginationOptions options = null)
        {
            options?.ThrowOnInvalidConfiguration();
            return new ApplyPaginationByClientRequestObservable<TSource>(
                options ?? new PaginationOptions(), observable);
        }

        private sealed class ApplyPaginationByClientRequestObservable<TSource>
            : Operator<IQueryable<TSource>, IQueryable<TSource>>
        {
            private readonly PaginationOptions options;

            public ApplyPaginationByClientRequestObservable(
                PaginationOptions options,
                IProviderObservable<IQueryable<TSource>> observable)
                : base(observable)
            {
                this.options = options;
            }

            protected override IObserver<IQueryable<TSource>> Create(
                IObserver<IQueryable<TSource>> observer, IDisposable disposable)
            {
                var interpreter = this.ServiceProvider
                    .GetService<IPaginationByClientRequestInterpreter>();
                var paginationInfoStorage = this.ServiceProvider
                    .GetService<IScopedStorage<PaginationInfo>>();
                return new ApplyPaginationByClientRequestObserver(
                    this.options, interpreter, paginationInfoStorage, observer, disposable);
            }

            private sealed class ApplyPaginationByClientRequestObserver : SafeObserver
            {
                private readonly PaginationOptions options;
                private readonly IPaginationByClientRequestInterpreter interpreter;
                private readonly IScopedStorage<PaginationInfo> paginationInfoStorage;

                public ApplyPaginationByClientRequestObserver(
                    PaginationOptions options,
                    IPaginationByClientRequestInterpreter interpreter,
                    IScopedStorage<PaginationInfo> paginationInfoStorage,
                    IObserver<IQueryable<TSource>> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.options = options;
                    this.interpreter = interpreter;
                    this.paginationInfoStorage = paginationInfoStorage;
                }

                protected override IQueryable<TSource> SafeOnNext(IQueryable<TSource> value)
                {
                    var paginationRequest = this.interpreter.ParseRequestQuery();
                    var paginationValues = new PaginationValues
                    {
                        Offset = paginationRequest.Offset ?? 0,
                        Limit = this.ResolveLimit(paginationRequest),
                    };
                    this.CalculateMetaInfo(value, paginationValues);

                    return value
                        .Skip(paginationValues.Offset)
                        .Take(paginationValues.Limit);
                }

                private int ResolveLimit(PaginationRequest request)
                {
                    var limit = request.Limit ?? this.options.DefaultLimit;
                    return limit > this.options.MaxLimit ? this.options.MaxLimit : limit;
                }

                private void CalculateMetaInfo(
                    IQueryable<TSource> queryable, PaginationValues paginationValues)
                {
                    var count = queryable.Count();
                    this.paginationInfoStorage.Value = new PaginationInfo(
                        count, paginationValues.Offset, paginationValues.Limit);
                }

                private class PaginationValues
                {
                    public int Offset { get; set; }

                    public int Limit { get; set; }
                }
            }
        }
    }
}
