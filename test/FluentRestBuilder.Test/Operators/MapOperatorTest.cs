// <copyright file="MapOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class MapOperatorTest
    {
        [Fact]
        public async Task TestMapping()
        {
            const string first = "first";
            const string append = "append";
            const string expected = first + append;

            var observable = new SingleObservable<string>(
                first, new EmptyServiceProvider())
                .Map(s => s + append);
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = new SingleObservable<string>(
                    string.Empty, new EmptyServiceProvider())
                .Map<string, string>(s => throw new InvalidOperationException());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<MapOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .Map<string, string>(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<MapOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
