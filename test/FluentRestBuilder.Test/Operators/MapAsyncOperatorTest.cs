// <copyright file="MapAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using FluentRestBuilder.Operators;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class MapAsyncOperatorTest
    {
        [Fact]
        public async Task TestMapping()
        {
            const string first = "first";
            const string append = "append";
            const string expected = first + append;

            var observable = new SingleObservable<string>(
                    first, new EmptyServiceProvider())
                .MapAsync(async s =>
                {
                    await Task.Delay(100);
                    return first + append;
                });
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = new SingleObservable<string>(
                    string.Empty, new EmptyServiceProvider())
                .MapAsync<string, string>(async s =>
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
            collection.AddTransient<MapAsyncOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .MapAsync<string, string>(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<MapAsyncOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
