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

            protected override IObserver<TSource> Create(IObserver<TSource> observer) =>
                new DoAsyncObserver(this.action, observer, this);

            private sealed class DoAsyncObserver : Observer
            {
                private readonly Func<TSource, Task> action;
                private Task continuationTask;

                public DoAsyncObserver(
                    Func<TSource, Task> action,
                    IObserver<TSource> child,
                    Operator<TSource, TSource> @operator)
                    : base(child, @operator)
                {
                    this.action = action;
                }

                public override void OnNext(TSource value)
                {
                    var task = Task.Run(() => this.action(value));
                    this.continuationTask = task.ContinueWith(t => this.OnTaskDone(t, value));
                }

                public override void OnCompleted()
                {
                    if (this.continuationTask != null)
                    {
                        this.continuationTask.ContinueWith(t => base.OnCompleted());
                    }
                    else
                    {
                        base.OnCompleted();
                    }
                }

                private void OnTaskDone(Task task, TSource value)
                {
                    if (task.Exception != null)
                    {
                        this.OnError(task.Exception.InnerException);
                    }
                    else
                    {
                        this.EmitNext(value);
                    }
                }
            }
        }
    }
}
