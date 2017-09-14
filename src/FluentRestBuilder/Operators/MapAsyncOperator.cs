// <copyright file="MapAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Operators;

    public static class MapAsyncOperator
    {
        /// <summary>
        /// Asynchronously map the received value to the desired output.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the output.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TTarget> MapAsync<TSource, TTarget>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task<TTarget>> mapping) =>
            new MapAsyncObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapAsyncObservable<TSource, TTarget> : Operator<TSource, TTarget>
        {
            private readonly Func<TSource, Task<TTarget>> mapping;

            public MapAsyncObservable(Func<TSource, Task<TTarget>> mapping, IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(mapping, nameof(mapping));
                this.mapping = mapping;
            }

            protected override IObserver<TSource> Create(
                IObserver<TTarget> observer, IDisposable disposable) =>
                new MapAsyncObserver(this.mapping, observer, disposable);

            private sealed class MapAsyncObserver : SafeAsyncObserver
            {
                private readonly Func<TSource, Task<TTarget>> mapping;

                public MapAsyncObserver(
                    Func<TSource, Task<TTarget>> mapping,
                    IObserver<TTarget> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                }

                protected override async Task SafeOnNext(TSource value)
                {
                    var newValue = await this.mapping(value);
                    this.EmitNext(newValue);
                }
            }
        }
    }
}
