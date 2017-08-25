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
        private readonly IServiceProvider serviceProvider;
        private ImmutableList<IObserver<T>> observers = ImmutableList<IObserver<T>>.Empty;
        private Task<T> valueTask;
        private bool isDone;
        private bool isDisposed;

        public AsyncSingleObservable(Func<Task<T>> valueTaskFactory, IServiceProvider serviceProvider)
        {
            Check.IsNull(valueTaskFactory, nameof(valueTaskFactory));
            Check.IsNull(serviceProvider, nameof(serviceProvider));
            this.valueFactory = valueTaskFactory;
            this.serviceProvider = serviceProvider;
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

        public void Dispose()
        {
            lock (this.gate)
            {
                this.isDisposed = true;
                this.observers = null;
                this.valueTask = null;
            }
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

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

            this.valueTask = this.valueFactory();
            this.valueTask.ContinueWith(this.OnTaskCompletion);
            if (this.valueTask.Status == TaskStatus.Created)
            {
                this.valueTask.Start();
            }
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
            }
            else
            {
                observer.OnError(this.UnwrapExceptionFromTask());
            }

            observer.OnCompleted();
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
