// <copyright file="EntityValidationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.EntityValidation
{
    using System.Threading.Tasks;
    using Builder;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class EntityValidationPipeTest : TestBaseWithServiceProvider
    {
        private readonly Source<Entity> source;
        private readonly Entity entity = new Entity { Id = 1, Name = "test" };

        public EntityValidationPipeTest()
        {
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterEntityValidationPipe()
                .Services
                .BuildServiceProvider();
            this.source = new Source<Entity>(this.entity, provider);
        }

        [Fact]
        public async Task TestInvalidValidation()
        {
            var result = await this.source
                .InvalidWhen(e => e.Id == this.entity.Id, StatusCodes.Status403Forbidden, "error")
                .ToMockResultPipe()
                .Execute();
            Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(
                StatusCodes.Status403Forbidden,
                ((ObjectResult)result).StatusCode.GetValueOrDefault());
        }

        [Fact]
        public async Task TestValidValidation()
        {
            var result = await this.source
                .InvalidWhen(e => e.Id == this.entity.Id + 1, StatusCodes.Status403Forbidden)
                .ToObjectResultOrDefault();
            Assert.Equal(this.entity.Id, result.Id);
        }
    }
}
