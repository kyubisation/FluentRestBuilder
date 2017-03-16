// <copyright file="NoContentResultTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Results.NoContent
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

    public class NoContentResultTest : IDisposable
    {
        private readonly MockController controller;

        public NoContentResultTest()
        {
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterSource()
                .RegisterNoContentResult()
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
                .ToNoContentResult();
            Assert.IsType<NoContentResult>(result);
            var objectResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
