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

            protected override IObserver<TFrom> Create(IObserver<TFrom> observer) =>
                new DoObserver(this.action, observer, this);

            private sealed class DoObserver : Observer
            {
                private readonly Action<TFrom> action;

                public DoObserver(
                    Action<TFrom> action,
                    IObserver<TFrom> child,
                    Operator<TFrom, TFrom> @operator)
                    : base(child, @operator)
                {
                    this.action = action;
                }

                public override void OnNext(TFrom value)
                {
                    try
                    {
                        this.action(value);
                        this.EmitNext(value);
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
