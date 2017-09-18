// <copyright file="MapOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MapOperatorTest
    {
        [Fact]
        public async Task TestMapping()
        {
            const string first = "first";
            const string append = "append";
            const string expected = first + append;

            var observable = Observable.Single(first)
                .Map(s => s + append);
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = Observable.Single(string.Empty)
                .Map<string, string>(s => throw new InvalidOperationException());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<MapOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .Map<string, string>(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<MapOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
