// <copyright file="CreatedEntityResultTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Results.CreatedEntity
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

    public class CreatedEntityResultTest : IDisposable
    {
        private readonly MockController controller;

        public CreatedEntityResultTest()
        {
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterCreatedEntityResult()
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
                .ToCreatedAtRouteResult(string.Empty, e => new { id = e.Id });
            Assert.IsType<CreatedAtRouteResult>(result);
            var objectResult = (ObjectResult)result;
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
            Assert.Equal(entity, objectResult.Value as Entity, new PropertyComparer<Entity>());
        }
    }
}
