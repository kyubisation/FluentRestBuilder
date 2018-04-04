// <copyright file="Operator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Disposables;

    public abstract class Operator<TSource, TTarget> : IProviderObservable<TTarget>
    {
        private readonly IProviderObservable<TSource> observable;

        protected Operator(IProviderObservable<TSource> observable)
        {
            Check.IsNull(observable, nameof(observable));
            this.observable = observable;
            this.ServiceProvider = this.observable.ServiceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public IDisposable Subscribe(IObserver<TTarget> observer)
        {
            var disposableCollection = new DisposableCollection();
            var operatorObserver = this.Create(observer, disposableCollection);
            var subscription = this.observable.Subscribe(operatorObserver);
            disposableCollection.Add(subscription);
            return disposableCollection;
        }

        protected abstract IObserver<TSource> Create(
            IObserver<TTarget> observer, IDisposable disposable);

        protected abstract class SafeAsyncObserver : Observer
        {
            private Task valueTask = Task.CompletedTask;
            private ImmutableList<TSource> values = ImmutableList<TSource>.Empty;
            private bool isDone;
            private Exception exception;

            protected SafeAsyncObserver(IObserver<TTarget> child, IDisposable disposable)
                : base(child, disposable)
            {
            }

            public override void OnNext(TSource value)
            {
                if (this.isDone || this.exception != null)
                {
                    return;
                }

                this.values = this.values.Add(value);
                if (this.valueTask.IsCompleted)
                {
                    this.valueTask = Task.Run(this.RunSafeOnNext);
                }
            }

            public override void OnError(Exception error)
            {
                this.exception = error;
                if (this.values.IsEmpty)
                {
                    base.OnError(error);
                }
            }

            public override void OnCompleted()
            {
                this.isDone = true;
                if (this.values.IsEmpty)
                {
                    base.OnCompleted();
                }
            }

            protected abstract Task<TTarget> SafeOnNext(TSource value);

            protected void EmitError(Exception error)
            {
                this.values = ImmutableList<TSource>.Empty;
                this.OnError(error);
            }

            private async Task RunSafeOnNext()
            {
                while (!this.values.IsEmpty)
                {
                    await this.RunNextValue();
                }

                if (this.exception != null)
                {
                    base.OnError(this.exception);
                }
                else if (this.isDone)
                {
                    base.OnCompleted();
                }
            }

            private async Task RunNextValue()
            {
                try
                {
                    var value = this.values.First();
                    var result = await this.SafeOnNext(value);
                    this.Child?.OnNext(result);
                    this.values = this.values.RemoveAt(0);
                }
                catch (Exception e)
                {
                    this.EmitError(e);
                }
            }
        }

        protected abstract class SafeObserver : Observer
        {
            protected SafeObserver(IObserver<TTarget> child, IDisposable disposable)
                : base(child, disposable)
            {
            }

            public override void OnNext(TSource value)
            {
                try
                {
                    var next = this.SafeOnNext(value);
                    this.Child?.OnNext(next);
                }
                catch (Exception e)
                {
                    this.OnError(e);
                }
            }

            protected abstract TTarget SafeOnNext(TSource value);
        }

        protected abstract class Observer : IObserver<TSource>, IDisposable
        {
            private IDisposable disposable;

            protected Observer(IObserver<TTarget> child, IDisposable disposable)
            {
                this.Child = child;
                this.disposable = disposable;
            }

            protected IObserver<TTarget> Child { get; private set; }

            public abstract void OnNext(TSource value);

            public virtual void OnError(Exception error)
            {
                this.Child?.OnError(error);
                this.Dispose();
            }

            public virtual void OnCompleted()
            {
                this.Child?.OnCompleted();
                this.Dispose();
            }

            public void Dispose()
            {
                Interlocked.Exchange(ref this.disposable, null)?.Dispose();
                this.Child = null;
            }

            [Obsolete("Use the Child attribute instead.")]
            protected void EmitNext(TTarget value) => this.Child?.OnNext(value);
        }
    }
}
