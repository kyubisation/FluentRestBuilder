// <copyright file="ToActionResultOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ActionResult
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ActionResult;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ToActionResultOperatorTest
    {
        public static IEnumerable<object[]> ClientRequestErrorStatusCodes() =>
            Enumerable.Range(400, 10)
                .Select(f => new object[] { f });

        [Fact]
        public async Task TestOkOperator()
        {
            const string expected = "expected";
            var observable = Observable.Single(expected)
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(expected, objectResult.Value);
        }

        [Theory]
        [MemberData(nameof(ClientRequestErrorStatusCodes))]
        public async Task TestValidationFailureWithError(int statusCode)
        {
            const string error = "error";
            var observable = Observable.Single("expected")
                .InvalidWhen(s => true, statusCode, error)
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsAssignableFrom<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(statusCode, objectResult.StatusCode);
            Assert.Equal(error, objectResult.Value);
        }

        [Theory]
        [MemberData(nameof(ClientRequestErrorStatusCodes))]
        public async Task TestValidationFailureWithoutError(int statusCode)
        {
            var observable = Observable.Single("expected")
                .InvalidWhen(s => true, statusCode)
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            var objectResult = result as StatusCodeResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(statusCode, objectResult.StatusCode);
        }

        [Fact]
        public async Task TestException()
        {
            const string expected = "expected";
            var observable = Observable.Single(expected)
                .Do(s => throw new InvalidOperationException())
                .ToActionResult(s => new OkObjectResult(s));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<ToActionResultOperatorTest>()
                .BuildServiceProvider();
            var observable = Observable.Single(string.Empty, serviceProvider)
                .ToActionResult(s => new OkObjectResult(s));
            var instance = observable.ServiceProvider.GetService<ToActionResultOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
