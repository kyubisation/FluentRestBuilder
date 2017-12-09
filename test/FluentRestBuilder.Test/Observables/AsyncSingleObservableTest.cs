// <copyright file="AsyncSingleObservableTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Observables
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AsyncSingleObservableTest
    {
        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<SingleObservableTest>()
                .BuildServiceProvider();
            var single = Observable.AsyncSingle(() => string.Empty, serviceProvider);
            var instance = single.ServiceProvider.GetService<SingleObservableTest>();
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task TestFunc()
        {
            const string expected = "expected";
            var single = Observable.AsyncSingle(() => expected);
            Assert.Equal(expected, await single);
        }

        [Fact]
        public async Task TestTask()
        {
            const string expected = "expected";
            var single = Observable.AsyncSingle(async () => await Task.FromResult(expected));
            Assert.Equal(expected, await single);
        }

        [Fact]
        public async Task TestLazy()
        {
            const string expected = "expected";
            var lazy = new Lazy<string>(() => expected);
            var single = Observable.AsyncSingle(lazy);
            Assert.False(lazy.IsValueCreated);
            Assert.Equal(expected, await single);
            Assert.True(lazy.IsValueCreated);
        }

        [Fact]
        public async Task TestDisposing()
        {
            const string expected = "expected";
            var single = Observable.AsyncSingle(() => expected);
            Assert.Equal(expected, await single);
            var disposable = (IDisposable)single;
            disposable.Dispose();
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => await single);
        }

        [Fact]
        public async Task TestSequentialAwait()
        {
            const string expected = "expected";
            var single = Observable.AsyncSingle(() => expected);
            Assert.Equal(expected, await single);
            Assert.Equal(expected, await single);
        }

        [Fact]
        public async Task TestException()
        {
            var single = Observable.AsyncSingle(
                (Func<string>)(() => throw new InvalidOperationException()));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await single);
        }

        [Fact]
        public async Task TestUnsubscription()
        {
            const string expected = "expected";
            var taskSource = new TaskCompletionSource<string>();
            var single = Observable.AsyncSingle(async () => await taskSource.Task);
            var disposable = single.Subscribe(new MockObserver<string>());
            disposable.Dispose();
            disposable.Dispose();
            taskSource.SetResult(expected);
            Assert.Equal(expected, await single);
        }

        private sealed class MockObserver<TSource> : IObserver<TSource>
        {
            public void OnCompleted() => throw new InvalidOperationException();

            public void OnError(Exception error) => throw new InvalidOperationException();

            public void OnNext(TSource value) => throw new InvalidOperationException();
        }
    }
}
