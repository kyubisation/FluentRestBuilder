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
        public async Task TestException()
        {
            var single = Observable.AsyncSingle(
                (Func<string>)(() => throw new InvalidOperationException()));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await single);
        }
    }
}
