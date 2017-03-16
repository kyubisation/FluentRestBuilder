// <copyright file="MappingBuilderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping
{
    using System;
    using FluentRestBuilder.Builder;
    using FluentRestBuilder.Mocks.EntityFramework;
    using HypertextApplicationLanguage.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class MappingBuilderTest
    {
        private readonly IServiceProvider provider;

        public MappingBuilderTest()
        {
            this.provider = new FluentRestBuilderConfiguration(new ServiceCollection())
                .AddHAL()
                .Services
                .BuildServiceProvider();
        }

        [Fact]
        public void TestEmptyCase()
        {
            var builder = new MappingBuilder<Entity>(
                new MapperFactory(this.provider),
                new MapperFactory<Entity>(this.provider));
            var mapping = builder.UseMapper(f => f.Resolve<EntityResponse>());
            var entity = new Entity
            {
                Id = 1,
                Name = $"name {nameof(this.TestEmptyCase)}",
                Description = $"description {nameof(this.TestEmptyCase)}"
            };
            var response = mapping(entity);
            Assert.Equal(entity.Id, response.Id);
            Assert.Equal(entity.Name, response.Name);
            Assert.Equal(entity.Description, response.Description);
        }

        [Fact]
        public void TestWithEmbedding()
        {
            const string embedKey = "key";
            var builder = new MappingBuilder<Entity>(
                new MapperFactory(this.provider),
                new MapperFactory<Entity>(this.provider));
            var mapping = builder.Embed(embedKey, e => e.Name)
                .UseMapper(f => f.Resolve<EntityResponse>());
            var entity = new Entity
            {
                Id = 1,
                Name = $"name {nameof(this.TestWithEmbedding)}",
                Description = $"description {nameof(this.TestWithEmbedding)}"
            };
            var result = mapping(entity);
            Assert.True(result.Embedded.ContainsKey(embedKey));
            Assert.Equal(entity.Name, result.Embedded[embedKey]);
        }
    }
}
