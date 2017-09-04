// <copyright file="DoOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;

    public static class DoOperator
    {
        /// <summary>
        /// Perform an action on the received value.
        /// </summary>
        /// <typeparam name="TFrom">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TFrom> Do<TFrom>(
            this IProviderObservable<TFrom> observable,
            Action<TFrom> action) =>
            new DoObservable<TFrom>(action, observable);

        private sealed class DoObservable<TFrom> : Operator<TFrom, TFrom>
        {
            private readonly Action<TFrom> action;

            public DoObservable(Action<TFrom> action, IProviderObservable<TFrom> observable)
                : base(observable)
            {
                Check.IsNull(action, nameof(action));
                this.action = action;
            }

            protected override IObserver<TFrom> Create(
                IObserver<TFrom> observer, IDisposable disposable) =>
                new DoObserver(this.action, observer, disposable);

            private sealed class DoObserver : SafeObserver
            {
                private readonly Action<TFrom> action;

                public DoObserver(
                    Action<TFrom> action,
                    IObserver<TFrom> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                }

                protected override void SafeOnNext(TFrom value)
                {
                    this.action(value);
                    this.EmitNext(value);
                }
            }
        }
    }
}
