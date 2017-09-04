// <copyright file="DoOperatorTest.cs" company="Kyubisation">
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

    public class DoOperatorTest
    {
        [Fact]
        public async Task TestDo()
        {
            const string expected = "expected";
            var value = string.Empty;

            var observable = new SingleObservable<string>(
                    expected, new EmptyServiceProvider())
                .Do(s => value = s);
            Assert.Equal(expected, await observable);
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = new SingleObservable<string>(
                    string.Empty, new EmptyServiceProvider())
                .Do(s => throw new InvalidOperationException());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<DoOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .Do(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<DoOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
