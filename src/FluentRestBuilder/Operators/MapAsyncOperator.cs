// <copyright file="MapAsyncOperator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators
{
    using System;
    using System.Threading.Tasks;

    public static class MapAsyncOperator
    {
        /// <summary>
        /// Asynchronously map the received value to the desired output.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the output.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TTarget> MapAsync<TSource, TTarget>(
            this IProviderObservable<TSource> observable,
            Func<TSource, Task<TTarget>> mapping) =>
            new MapAsyncObservable<TSource, TTarget>(mapping, observable);

        private sealed class MapAsyncObservable<TSource, TTarget> : Operator<TSource, TTarget>
        {
            private readonly Func<TSource, Task<TTarget>> mapping;

            public MapAsyncObservable(Func<TSource, Task<TTarget>> mapping, IProviderObservable<TSource> observable)
                : base(observable)
            {
                Check.IsNull(mapping, nameof(mapping));
                this.mapping = mapping;
            }

            protected override IObserver<TSource> Create(IObserver<TTarget> observer) =>
                new MapAsyncObserver(this.mapping, observer, this);

            private sealed class MapAsyncObserver : Observer
            {
                private readonly Func<TSource, Task<TTarget>> mapping;
                private Task continuationTask;

                public MapAsyncObserver(
                    Func<TSource, Task<TTarget>> mapping,
                    IObserver<TTarget> child,
                    Operator<TSource, TTarget> @operator)
                    : base(child, @operator)
                {
                    this.mapping = mapping;
                }

                public override void OnNext(TSource value)
                {
                    var task = Task.Run(() => this.mapping(value));
                    this.continuationTask = task.ContinueWith(this.OnTaskDone);
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

                private void OnTaskDone(Task<TTarget> task)
                {
                    if (task.Exception != null)
                    {
                        this.OnError(task.Exception.InnerException);
                    }
                    else
                    {
                        this.EmitNext(task.Result);
                    }
                }
            }
        }
    }
}
