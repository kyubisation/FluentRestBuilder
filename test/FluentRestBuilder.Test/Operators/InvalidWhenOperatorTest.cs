// <copyright file="InvalidWhenOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using FluentRestBuilder.Operators;
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class InvalidWhenOperatorTest
    {
        [Fact]
        public async Task TestValidCase()
        {
            const string expected = "expected";
            var observable = new SingleObservable<string>(expected, new EmptyServiceProvider())
                .InvalidWhen(s => false, 400);
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestInvalidCase()
        {
            const string expected = "expected";
            const int statusCode = 400;
            var observable = new SingleObservable<string>(expected, new EmptyServiceProvider())
                .InvalidWhen(s => true, statusCode);
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await observable);
            Assert.Equal(statusCode, exception.StatusCode);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<InvalidWhenOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .InvalidWhen(s => s == string.Empty, 400);
            var instance = observable.ServiceProvider.GetService<InvalidWhenOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
