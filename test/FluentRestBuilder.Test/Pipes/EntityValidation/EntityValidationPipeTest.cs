// <copyright file="EntityValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.EntityValidation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class EntityValidationPipeTest
    {
        private readonly Source<Entity> source;
        private readonly Entity entity = new Entity { Id = 1, Name = "test" };

        public EntityValidationPipeTest()
        {
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterEntityValidationPipe()
                .RegisterMappingPipe()
                .Services
                .BuildServiceProvider();
            this.source = new Source<Entity>(this.entity, provider);
        }

        [Theory]
        [ClassData(typeof(TheoryData))]
        public async Task TestInvalidCases(
            Func<IOutputPipe<Entity>, IOutputPipe<Entity>> creator, int statusCode, bool hasErrorMessage)
        {
            var result = await creator(this.source)
                .ToMockResultPipe()
                .Execute();
            if (hasErrorMessage)
            {
                AssertObjectResultWithStatusCode(result, statusCode);
            }
            else
            {
                AssertStatusCodeResultWithStatusCode(result, statusCode);
            }
        }

        [Fact]
        public async Task TestValidValidation()
        {
            var result = await this.source
                .InvalidWhen(e => e.Id == this.entity.Id + 1, StatusCodes.Status403Forbidden)
                .ToObjectResultOrDefault();
            Assert.Equal(this.entity.Id, result.Id);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void AssertObjectResultWithStatusCode(IActionResult result, int statusCode)
        {
            Assert.IsAssignableFrom<ObjectResult>(result);
            var objectResult = (ObjectResult)result;
            Assert.Equal(statusCode, objectResult.StatusCode);
        }

        // ReSharper disable once UnusedParameter.Local
        private static void AssertStatusCodeResultWithStatusCode(
            IActionResult result, int statusCode)
        {
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Assert.Equal(statusCode, objectResult.StatusCode);
        }

        private class TheoryData : List<object[]>
        {
            public TheoryData()
            {
                this.AddWithError(s => s.InvalidWhen(e => true, 400, "error"), 400)
                    .AddWithoutError(s => s.InvalidWhen(e => true, 400), 400)
                    .AddWithError(s => s.BadRequestWhen(e => true, "error"), 400)
                    .AddWithError(s => s.BadRequestWhen(() => true, "error"), 400)
                    .AddWithoutError(s => s.BadRequestWhen(e => true), 400)
                    .AddWithoutError(s => s.BadRequestWhen(() => true), 400)
                    .AddWithError(s => s.ForbiddenWhen(e => true, "error"), 403)
                    .AddWithError(s => s.ForbiddenWhen(() => true, "error"), 403)
                    .AddWithoutError(s => s.ForbiddenWhen(e => true), 403)
                    .AddWithoutError(s => s.ForbiddenWhen(() => true), 403)
                    .AddWithError(s => s.NotFoundWhen(e => true, "error"), 404)
                    .AddWithError(s => s.NotFoundWhen(() => true, "error"), 404)
                    .AddWithoutError(s => s.NotFoundWhen(e => true), 404)
                    .AddWithoutError(s => s.NotFoundWhen(() => true), 404)
                    .AddWithError(s => s.Map(e => (Entity)null).NotFoundWhenEmpty("error"), 404)
                    .AddWithoutError(s => s.Map(e => (Entity)null).NotFoundWhenEmpty(), 404)
                    .AddWithError(s => s.GoneWhen(e => true, "error"), 410)
                    .AddWithError(s => s.GoneWhen(() => true, "error"), 410)
                    .AddWithoutError(s => s.GoneWhen(e => true), 410)
                    .AddWithoutError(s => s.GoneWhen(() => true), 410);
            }

            private TheoryData AddWithError(
                Func<IOutputPipe<Entity>, IOutputPipe<Entity>> creator, int statusCode)
            {
                this.Add(new object[] { creator, statusCode, true });
                return this;
            }

            private TheoryData AddWithoutError(
                Func<IOutputPipe<Entity>, IOutputPipe<Entity>> creator, int statusCode)
            {
                this.Add(new object[] { creator, statusCode, false });
                return this;
            }
        }
    }
}
