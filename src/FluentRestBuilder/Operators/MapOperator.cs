// <copyright file="MapOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Operators;

    public static class MapOperator
    {
        /// <summary>
        /// Map the received value to the desired output.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the output.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TTarget> Map<TSource, TTarget>(
            this IProviderObservable<TSource> observable,
            Func<TSource, TTarget> mapping) =>
            new MapObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapObservable<TSource, TTarget> : Operator<TSource, TTarget>
        {
            private readonly Func<TSource, TTarget> mapping;

            public MapObservable(Func<TSource, TTarget> mapping, IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(mapping, nameof(mapping));
                this.mapping = mapping;
            }

            protected override IObserver<TSource> Create(
                IObserver<TTarget> observer, IDisposable disposable) =>
                new MapObserver(this.mapping, observer, disposable);

            private sealed class MapObserver : SafeObserver
            {
                private readonly Func<TSource, TTarget> mapping;

                public MapObserver(
                    Func<TSource, TTarget> mapping,
                    IObserver<TTarget> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.mapping = mapping;
                }

                protected override void SafeOnNext(TSource value)
                {
                    var newValue = this.mapping(value);
                    this.EmitNext(newValue);
                }
            }
        }
    }
}
