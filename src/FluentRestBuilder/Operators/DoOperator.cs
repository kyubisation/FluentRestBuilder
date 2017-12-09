// <copyright file="DoOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Operators;

    public static class DoOperator
    {
        /// <summary>
        /// Perform an action on the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Do<TSource>(
            this IProviderObservable<TSource> observable,
            Action<TSource> action) =>
            new DoObservable<TSource>(action, observable);

        private sealed class DoObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Action<TSource> action;

            public DoObservable(Action<TSource> action, IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(action, nameof(action));
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable) =>
                new DoObserver(this.action, observer, disposable);

            private sealed class DoObserver : SafeObserver
            {
                private readonly Action<TSource> action;

                public DoObserver(
                    Action<TSource> action,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                }

                protected override TSource SafeOnNext(TSource value)
                {
                    this.action(value);
                    return value;
                }
            }
        }
    }
}
