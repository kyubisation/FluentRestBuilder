// <copyright file="OkResultTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Results.Ok
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class OkResultTest : IDisposable
    {
        private readonly MockController controller;

        public OkResultTest()
        {
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterOkResult()
                .Services
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        public void Dispose()
        {
            this.controller.Dispose();
        }

        [Fact]
        public async Task TestBasicUsage()
        {
            var entity = new Entity { Id = 1 };
            var result = await this.controller.FromSource(entity)
                .ToOkResult();
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.Equal(entity, objectResult.Value as Entity, new PropertyComparer<Entity>());
        }
    }
}
