﻿// <copyright file="CurrentUserHasOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.Validation
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using FluentRestBuilder.Operators.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Storage;
    using Xunit;

    public class CurrentUserHasOperatorTest
    {
        [Fact]
        public async Task TestValidCase()
        {
            const string expected = "expected";
            const string claimType = "claim";
            var collection = new ServiceCollection();
            collection.AddTransient(
                s => CreateHttpContextWithPrincipal(
                    p => p.AddClaim(claimType, expected)));
            var observable = new SingleObservable<string>(
                expected, collection.BuildServiceProvider())
                .CurrentUserHas((p, i) => p.HasClaim(claimType, expected));
            Assert.Equal(expected, await observable);
        }

        [Fact]
        public async Task TestInvalidCase()
        {
            const string expected = "expected";
            var collection = new ServiceCollection();
            collection.AddTransient(s => CreateHttpContextWithPrincipal());
            var observable = new SingleObservable<string>(
                expected, collection.BuildServiceProvider())
                .CurrentUserHas((p, i) => p.HasClaim("claim", expected));
            var exception = await Assert.ThrowsAsync<ValidationException>(
                async () => await observable);
            Assert.Equal(StatusCodes.Status403Forbidden, exception.StatusCode);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<CurrentUserHasOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .CurrentUserHas((p, s) => s == string.Empty);
            var instance = observable.ServiceProvider.GetService<CurrentUserHasOperatorTest>();
            Assert.NotNull(instance);
        }

        private static IScopedStorage<HttpContext> CreateHttpContextWithPrincipal(
            Action<MockPrincipal> builder = null)
        {
            var principal = new MockPrincipal();
            builder?.Invoke(principal);
            return new ScopedStorage<HttpContext>
            {
                Value = new DefaultHttpContext
                {
                    User = principal,
                },
            };
        }
    }
}