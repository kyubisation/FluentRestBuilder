// <copyright file="InvalidWhenAsyncOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.Validation
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class InvalidWhenAsyncOperatorTest
    {
        [Fact]
        public async Task TestValidCase()
        {
            const string expected = "expected";
            var observable = Observable.Single(expected)
                .InvalidWhenAsync(
                    async s =>
                    {
                        await Task.Delay(100);
                        return false;
                    }, 400);
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestInvalidCase()
        {
            const string expected = "expected";
            const int statusCode = 400;
            var observable = Observable.Single(expected)
                .InvalidWhenAsync(
                    async s =>
                    {
                        await Task.Delay(100);
                        return true;
                    }, statusCode);
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await observable);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<InvalidWhenAsyncOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .InvalidWhenAsync(s => Task.FromResult(false), 400);
            var instance = observable.ServiceProvider.GetService<InvalidWhenAsyncOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
