// <copyright file="ToActionResultOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ActionResult
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using FluentRestBuilder.Operators.ActionResult;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class ToActionResultOperatorTest
    {
        [Fact]
        public async Task TestOkOperator()
        {
            const string expected = "expected";
            var observable = new SingleObservable<string>(expected, new EmptyServiceProvider())
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsType<OkObjectResult>(result);
            var objectResult = result as OkObjectResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(expected, objectResult.Value);
        }

        [Fact]
        public async Task TestValidationFailureWithError()
        {
            const string error = "error";
            const int statusCode = 400;
            var observable = new SingleObservable<string>("expected", new EmptyServiceProvider())
                .InvalidWhen(s => true, statusCode, error)
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsType<BadRequestObjectResult>(result);
            var objectResult = result as BadRequestObjectResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(statusCode, objectResult.StatusCode);
            Assert.Equal(error, objectResult.Value);
        }

        [Fact]
        public async Task TestValidationFailureWithoutError()
        {
            const int statusCode = 400;
            var observable = new SingleObservable<string>("expected", new EmptyServiceProvider())
                .InvalidWhen(s => true, statusCode)
                .ToActionResult(s => new OkObjectResult(s));
            var result = await observable;
            Assert.IsType<BadRequestResult>(result);
            var objectResult = result as BadRequestResult;
            Debug.Assert(objectResult != null, nameof(objectResult) + " != null");
            Assert.Equal(statusCode, objectResult.StatusCode);
        }

        [Fact]
        public async Task TestException()
        {
            const string expected = "expected";
            var observable = new SingleObservable<string>(expected, new EmptyServiceProvider())
                .Do(s => throw new InvalidOperationException())
                .ToActionResult(s => new OkObjectResult(s));
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await observable);
        }

        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<ToActionResultOperatorTest>();
            var observable = new SingleObservable<string>(
                    string.Empty, collection.BuildServiceProvider())
                .ToActionResult(s => new OkObjectResult(s));
            var instance = observable.ServiceProvider.GetService<ToActionResultOperatorTest>();
            Assert.NotNull(instance);
        }
    }
}
