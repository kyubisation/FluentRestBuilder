// <copyright file="DoOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class DoOperatorTest
    {
        [Fact]
        public async Task TestDo()
        {
            const string expected = "expected";
            var value = string.Empty;

            var observable = Observable.Single(expected)
                .Do(s => value = s);
            Assert.Equal(expected, await observable);
            Assert.Equal(expected, value);
        }

        [Fact]
        public async Task TestException()
        {
            var observable = Observable.Single(string.Empty)
                .Do(s => throw new InvalidOperationException());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<DoOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .Do(s => throw new InvalidOperationException());
            var instance = observable.ServiceProvider.GetService<DoOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
