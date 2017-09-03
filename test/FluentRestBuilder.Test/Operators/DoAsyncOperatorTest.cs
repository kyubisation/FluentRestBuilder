// <copyright file="DoAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using FluentRestBuilder.Operators;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DoAsyncOperatorTest
    {
        [Fact]
        public async Task TestDoAsync()
        {
            const string expected = "expected";
            var value = string.Empty;

            var observable = new SingleObservable<string>(
                    expected, new ServiceCollection().BuildServiceProvider())
                .DoAsync(async s =>
                {
                    await Task.Delay(100);
                    value = s;
                });
            Assert.Equal(expected, await observable);
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = new SingleObservable<string>(
                    string.Empty, new ServiceCollection().BuildServiceProvider())
                .DoAsync(async s =>
                {
                    await Task.Delay(100);
                    throw new InvalidOperationException();
                });
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<DoAsyncOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .DoAsync(async s =>
                {
                    await Task.Delay(100);
                    throw new InvalidOperationException();
                });
            var instance = observable.ServiceProvider.GetService<DoAsyncOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
