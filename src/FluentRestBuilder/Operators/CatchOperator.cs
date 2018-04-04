// <copyright file="CatchOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Operators;

    public static class CatchOperator
    {
        /// <summary>
        /// Catch an exception emitted from the previous observables or operators
        /// and return a new observable. This will only catch the exception if it is
        /// an instance of the declared exception type.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="handler">The function to handle the exception.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> Catch<TSource, TException>(
            this IProviderObservable<TSource> observable,
            Func<TException, IProviderObservable<TSource>> handler)
            where TException : Exception =>
            new CatchObservable<TSource, TException>(handler, observable);

        private sealed class CatchObservable<TSource, TException> : Operator<TSource, TSource>
            where TException : Exception
        {
            private readonly Func<TException, IProviderObservable<TSource>> handler;

            public CatchObservable(
                Func<TException, IProviderObservable<TSource>> handler,
                IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(handler, nameof(handler));
                this.handler = handler;
            }

            protected override IObserver<TSource> Create(
                IObserver<TSource> observer, IDisposable disposable) =>
                new CatchObserver(this.handler, observer, disposable);

            private sealed class CatchObserver : SafeObserver
            {
                private readonly Func<TException, IProviderObservable<TSource>> handler;

                public CatchObserver(
                    Func<TException, IProviderObservable<TSource>> handler,
                    IObserver<TSource> child,
                    IDisposable disposable)
                    : base(child, disposable)
                {
                    this.handler = handler;
                }

                public override void OnError(Exception error)
                {
                    if (error is TException exception)
                    {
                        this.TryHandler(exception);
                    }
                    else
                    {
                        base.OnError(error);
                    }
                }

                protected override TSource SafeOnNext(TSource value) => value;

                private void TryHandler(TException exception)
                {
                    try
                    {
                        var result = this.handler(exception);
                        result.Subscribe(new HandlerObserver(this));
                    }
                    catch (Exception e)
                    {
                        base.OnError(e);
                    }
                }

                private sealed class HandlerObserver : IObserver<TSource>
                {
                    private readonly CatchObserver parent;

                    public HandlerObserver(CatchObserver parent)
                    {
                        this.parent = parent;
                    }

                    public void OnCompleted()
                    {
                        this.parent.Child?.OnCompleted();
                        this.parent.Dispose();
                    }

                    public void OnError(Exception error)
                    {
                        this.parent.Child?.OnError(error);
                        this.parent.Dispose();
                    }

                    public void OnNext(TSource value) => this.parent.Child?.OnNext(value);
                }
            }
        }
    }
}
