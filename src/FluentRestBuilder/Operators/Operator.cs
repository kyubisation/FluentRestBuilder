// <copyright file="Operator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Threading;

    public abstract class Operator<TSource, TTarget> : IProviderObservable<TTarget>, IDisposable
    {
        private readonly IProviderObservable<TSource> observable;
        private IDisposable subscription;

        protected Operator(IProviderObservable<TSource> observable)
        {
            Check.IsNull(observable, nameof(observable));
            this.observable = observable;
            this.ServiceProvider = this.observable.ServiceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IDisposable Subscribe(IObserver<TTarget> observer)
        {
            var operatorObserver = this.Create(observer);
            this.subscription = this.observable.Subscribe(operatorObserver);
            return this;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Interlocked.Exchange(ref this.subscription, null)?.Dispose();
        }

        protected abstract IObserver<TSource> Create(IObserver<TTarget> observer);

        protected abstract class Observer : IObserver<TSource>
        {
            private readonly IObserver<TTarget> child;
            private readonly IDisposable @operator;

            protected Observer(IObserver<TTarget> child, Operator<TSource, TTarget> @operator = null)
            {
                this.child = child;
                this.@operator = @operator;
            }

            public abstract void OnNext(TSource value);

            public void OnError(Exception error)
            {
                this.child.OnError(error);
                this.@operator?.Dispose();
            }

            public virtual void OnCompleted()
            {
                this.child.OnCompleted();
                this.@operator?.Dispose();
            }

            protected void EmitNext(TTarget value) => this.child.OnNext(value);
        }
    }
}
