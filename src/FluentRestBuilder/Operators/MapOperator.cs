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
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
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

            protected override IObserver<TSource> Create(IObserver<TTarget> observer) =>
                new MapObserver(this.mapping, observer, this);

            private sealed class MapObserver : Observer
            {
                private readonly Func<TSource, TTarget> mapping;

                public MapObserver(
                    Func<TSource, TTarget> mapping,
                    IObserver<TTarget> child,
                    Operator<TSource, TTarget> @operator)
                    : base(child, @operator)
                {
                    this.mapping = mapping;
                }

                public override void OnNext(TSource value)
                {
                    try
                    {
                        var newValue = this.mapping(value);
                        this.EmitNext(newValue);
                    }
                    catch (Exception e)
                    {
                        this.OnError(e);
                    }
                }
            }
        }
    }
}
