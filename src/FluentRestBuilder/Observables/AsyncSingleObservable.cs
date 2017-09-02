// <copyright file="AsyncSingleObservable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Observables
{
    using System;
    using System.Collections.Immutable;
    using System.Threading.Tasks;
    using Disposables;

    public sealed class AsyncSingleObservable<T> : IProviderObservable<T>, IDisposable
    {
        private readonly object gate = new object();
        private readonly Func<Task<T>> valueFactory;
        private ImmutableList<IObserver<T>> observers = ImmutableList<IObserver<T>>.Empty;
        private Task<T> valueTask;
        private bool isDone;
        private bool isDisposed;

        public AsyncSingleObservable(
            Func<Task<T>> valueTaskFactory, IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            Check.IsNull(valueTaskFactory, nameof(valueTaskFactory));
            this.valueFactory = valueTaskFactory;
        }

        public AsyncSingleObservable(Func<T> valueFactory, IServiceProvider serviceProvider)
            : this(() => Task.FromResult(valueFactory()), serviceProvider)
        {
            Check.IsNull(valueFactory, nameof(valueFactory));
        }

        public AsyncSingleObservable(Lazy<T> lazyValue, IServiceProvider serviceProvider)
            : this(() => lazyValue.Value, serviceProvider)
        {
            Check.IsNull(lazyValue, nameof(lazyValue));
        }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            lock (this.gate)
            {
                this.isDisposed = true;
                this.observers = null;
                this.valueTask = null;
            }
        }

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            Check.IsNull(observer, nameof(observer));
            lock (this.gate)
            {
                this.CheckDisposed();
                if (!this.isDone)
                {
                    this.observers = this.observers.Add(observer);
                    this.InitializeTask();
                    return new Subscription(this, observer);
                }
            }

            this.EmitResult(observer);
            return Disposable.Empty;
        }

        private void CheckDisposed()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(typeof(AsyncSingleObservable<T>).Name);
            }
        }

        private void InitializeTask()
        {
            if (this.valueTask != null)
            {
                return;
            }

            this.valueTask = Task.Run(this.valueFactory);
            this.valueTask.ContinueWith(this.OnTaskCompletion);
        }

        private void OnTaskCompletion(Task<T> task)
        {
            this.isDone = true;
            this.observers.ForEach(this.EmitResult);
            this.observers = null;
        }

        private void EmitResult(IObserver<T> observer)
        {
            if (this.valueTask.Exception == null)
            {
                observer.OnNext(this.valueTask.Result);
                observer.OnCompleted();
            }
            else
            {
                observer.OnError(this.UnwrapExceptionFromTask());
            }
        }

        private Exception UnwrapExceptionFromTask()
        {
            return this.valueTask.Exception?.InnerException ?? this.valueTask.Exception;
        }

        private sealed class Subscription : IDisposable
        {
            private readonly AsyncSingleObservable<T> single;
            private IObserver<T> observer;

            public Subscription(AsyncSingleObservable<T> single, IObserver<T> observer)
            {
                this.single = single;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (this.observer == null)
                {
                    return;
                }

                lock (this.single.gate)
                {
                    if (!this.single.isDisposed && this.observer != null)
                    {
                        this.single.observers = this.single.observers.Remove(this.observer);
                        this.observer = null;
                    }
                }
            }
        }
    }
}
