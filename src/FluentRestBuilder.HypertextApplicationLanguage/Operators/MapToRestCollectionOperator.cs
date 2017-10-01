// <copyright file="MapToRestCollectionOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
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
        /// in an <see cref="RestEntityCollection"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the mapping result.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<RestEntityCollection> MapToRestCollection<TSource, TTarget>(
            this IProviderObservable<IEnumerable<TSource>> observable, Func<TSource, TTarget> mapping) =>
            new MapToRestCollectionObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapToRestCollectionObservable<TSource, TTarget>
            : Operator<IEnumerable<TSource>, RestEntityCollection>
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
                IObserver<RestEntityCollection> observer, IDisposable disposable)
            {
                var linkGenerator = this.ResolveLinkGenerator();
                var linkAggregator = this.ResolveLinkAggregator();
                var paginationMetaInfoStorage = this.ServiceProvider
                    .GetService<IScopedStorage<PaginationMetaInfo>>();
                return new MapToRestCollectionObserver(
                    this.mapping,
                    linkGenerator,
                    linkAggregator,
                    paginationMetaInfoStorage,
                    observer,
                    disposable);
            }

            private IRestCollectionLinkGenerator ResolveLinkGenerator()
            {
                var linkGenerator = this.ServiceProvider
                    .GetService<IRestCollectionLinkGenerator>();
                if (linkGenerator != null)
                {
                    return linkGenerator;
                }

                var httpContextStorage = this.ServiceProvider
                    .GetService<IScopedStorage<HttpContext>>();
                return new RestCollectionLinkGenerator(httpContextStorage);
            }

            private ILinkAggregator ResolveLinkAggregator() =>
                this.ServiceProvider.GetService<ILinkAggregator>() ?? new LinkAggregator();

            private sealed class MapToRestCollectionObserver : SafeObserver
            {
                private readonly Func<TSource, TTarget> mapping;
                private readonly IRestCollectionLinkGenerator linkGenerator;
                private readonly ILinkAggregator linkAggregator;
                private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;

                public MapToRestCollectionObserver(
                    Func<TSource, TTarget> mapping,
                    IRestCollectionLinkGenerator linkGenerator,
                    ILinkAggregator linkAggregator,
                    IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
                    IObserver<RestEntityCollection> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                    this.linkGenerator = linkGenerator;
                    this.linkAggregator = linkAggregator;
                    this.paginationMetaInfoStorage = paginationMetaInfoStorage;
                }

                protected override void SafeOnNext(IEnumerable<TSource> value)
                {
                    var restEntityCollection = new RestEntityCollection();
                    this.GenerateEmbeddedEntities(restEntityCollection, value);
                    this.GenerateLinks(restEntityCollection);
                    this.EmitNext(restEntityCollection);
                }

                private void GenerateEmbeddedEntities(
                    IRestEntity restEntityCollection, IEnumerable<TSource> entities)
                {
                    var mappedEntities = entities
                        .Select(e => this.mapping(e))
                        .ToList();
                    restEntityCollection._embedded.Add("items", mappedEntities);
                }

                private void GenerateLinks(IRestEntity restEntityCollection)
                {
                    var links = this.linkGenerator.GenerateLinks(this.paginationMetaInfoStorage.Value);
                    restEntityCollection._links = this.linkAggregator.BuildLinks(links);
                }
            }
        }
    }
}
