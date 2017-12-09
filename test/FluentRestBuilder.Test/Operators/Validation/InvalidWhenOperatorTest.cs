// <copyright file="InvalidWhenOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.Validation
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class InvalidWhenOperatorTest
    {
        [Fact]
        public async Task TestValidCase()
        {
            const string expected = "expected";
            var observable = Observable.Single(expected)
                .InvalidWhen(s => false, 400);
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestInvalidCase()
        {
            const string expected = "expected";
            const int statusCode = 400;
            var observable = Observable.Single(expected)
                .InvalidWhen(s => true, statusCode);
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await observable);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<InvalidWhenOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .InvalidWhen(s => s == string.Empty, 400);
            var instance = observable.ServiceProvider.GetService<InvalidWhenOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
