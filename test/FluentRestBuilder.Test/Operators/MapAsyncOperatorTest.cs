// <copyright file="MapAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MapAsyncOperatorTest
    {
        [Fact]
        public async Task TestMapping()
        {
            const string first = "first";
            const string append = "append";
            const string expected = first + append;

            var observable = Observable.Single(first)
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
            var observable = Observable.Single(string.Empty)
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
            var serviceProvider = new ServiceCollection()
                .AddTransient<MapAsyncOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .MapAsync<string, string>(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<MapAsyncOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
