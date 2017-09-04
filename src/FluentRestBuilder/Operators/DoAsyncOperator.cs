// <copyright file="DoAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Threading.Tasks;

    public static class DoAsyncOperator
    {
        /// <summary>
        /// Asynchronously perform an action on the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> DoAsync<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task> action) =>
            new DoAsyncObservable<TSource>(action, observable);

        private sealed class DoAsyncObservable<TSource> : Operator<TSource, TSource>
        {
            private readonly Func<TSource, Task> action;

            public DoAsyncObservable(
                Func<TSource, Task> action, IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(action, nameof(action));
                this.action = action;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable) =>
                new DoAsyncObserver(this.action, observer, disposable);

            private sealed class DoAsyncObserver : SafeAsyncObserver
            {
                private readonly Func<TSource, Task> action;

                public DoAsyncObserver(
                    Func<TSource, Task> action,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.action = action;
                }

                protected override async Task SafeOnNext(TSource value)
                {
                    await this.action(value);
                    this.EmitNext(value);
                }
            }
        }
    }
}
