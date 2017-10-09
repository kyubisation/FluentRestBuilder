// <copyright file="MapToRestCollectionOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Operators;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Operators;
    using Operators.ClientRequest;
    using Storage;

    public static class MapToRestCollectionOperator
    {
        /// <summary>
        /// Maps the entries of the received <see cref="IEnumerable{T}"/>
        /// according to the given mapping function and wraps the result
        /// in an <see cref="IRestEntity"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the mapping result.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IRestEntity> MapToRestCollection<TSource, TTarget>(
            this IProviderObservable<IEnumerable<TSource>> observable, Func<TSource, TTarget> mapping) =>
            new MapToRestCollectionObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapToRestCollectionObservable<TSource, TTarget>
            : Operator<IEnumerable<TSource>, IRestEntity>
        {
            private readonly Func<TSource, TTarget> mapping;

            public MapToRestCollectionObservable(
                Func<TSource, TTarget> mapping,
                IProviderObservable<IEnumerable<TSource>> observable)
                : base(observable)
            {
                this.mapping = mapping;
            }

            protected override IObserver<IEnumerable<TSource>> Create(
                IObserver<IRestEntity> observer, IDisposable disposable)
            {
                var generator = this.ResolveRestCollectionGenerator();
                var paginationInfoStorage = this.ServiceProvider
                    .GetService<IScopedStorage<PaginationInfo>>();
                return new MapToRestCollectionObserver(
                    this.mapping, generator, paginationInfoStorage, observer, disposable);
            }

            private IRestCollectionGenerator<TSource, TTarget> ResolveRestCollectionGenerator()
            {
                var generator = this.ServiceProvider
                    .GetService<IRestCollectionGenerator<TSource, TTarget>>();
                if (generator != null)
                {
                    return generator;
                }

                var linkHelper = this.ResolveLinkHelper();
                return new RestCollectionGenerator<TSource, TTarget>(linkHelper);
            }

            private ILinkHelper ResolveLinkHelper()
            {
                var linkHelper = this.ServiceProvider
                    .GetService<ILinkHelper>();
                if (linkHelper != null)
                {
                    return linkHelper;
                }

                var httpContextStorage = this.ServiceProvider
                    .GetService<IScopedStorage<HttpContext>>();
                return new LinkHelper(httpContextStorage);
            }

            private sealed class MapToRestCollectionObserver : SafeObserver
            {
                private readonly Func<TSource, TTarget> mapping;
                private readonly IRestCollectionGenerator<TSource, TTarget> restCollectionGenerator;
                private readonly IScopedStorage<PaginationInfo> paginationInfoStorage;

                public MapToRestCollectionObserver(
                    Func<TSource, TTarget> mapping,
                    IRestCollectionGenerator<TSource, TTarget> restCollectionGenerator,
                    IScopedStorage<PaginationInfo> paginationInfoStorage,
                    IObserver<IRestEntity> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                    this.restCollectionGenerator = restCollectionGenerator;
                    this.paginationInfoStorage = paginationInfoStorage;
                }

                protected override IRestEntity SafeOnNext(IEnumerable<TSource> value) =>
                    this.restCollectionGenerator.CreateCollection(
                        value, this.mapping, this.paginationInfoStorage.Value);
            }
        }
    }
}
