// <copyright file="Awaiter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class Awaiter
    {
        public static TaskAwaiter<TResult> GetAwaiter<TResult>(this IObservable<TResult> observable)
        {
            var awaiter = new AwaitObserver<TResult>();
            observable.Subscribe(awaiter);
            return awaiter.GetAwaiter();
        }

        public sealed class AwaitObserver<TResult> : IObserver<TResult>
        {
            private readonly TaskCompletionSource<TResult> taskCompletionSource =
                new TaskCompletionSource<TResult>();

            private TResult result;

            public TaskAwaiter<TResult> GetAwaiter() => this.taskCompletionSource.Task.GetAwaiter();

            public void OnCompleted() => this.taskCompletionSource.SetResult(this.result);

            public void OnError(Exception error) => this.taskCompletionSource.SetException(error);

            public void OnNext(TResult value) => this.result = value;
        }
    }
}
