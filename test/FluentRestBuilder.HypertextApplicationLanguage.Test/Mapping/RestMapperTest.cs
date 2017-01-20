// <copyright file="RestMapperTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping
{
    using System.Collections.Generic;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class RestMapperTest
    {
        [Fact]
        public void TestMapping()
        {
            var mapper = this.CreateMapper<EntityResponse>();
            var entity = new Entity
            {
                Id = 1,
                Name = "name",
                Description = "description"
            };
            var response = mapper.Map(entity);
            Assert.Equal(entity.Id, response.Id);
            Assert.Equal(entity.Name, response.Name);
            Assert.Equal(entity.Description, response.Description);
            Assert.Null(response.Embedded);
            Assert.Null(response.Links);
        }

        [Fact]
        public void TestEmbedding()
        {
            var mapper = this.CreateMapper<EntityResponse>();
            const string embedKey = "test";
            var response = mapper
                .Embed(embedKey, new Parent())
                .Map(new Entity());
            Assert.Null(response.Links);
            Assert.Equal(1, response.Embedded.Count);
            Assert.True(response.Embedded.ContainsKey(embedKey));
            var parent = response.Embedded[embedKey] as Parent;
            Assert.NotNull(parent);
        }

        [Fact]
        public void TestLinkGeneration()
        {
            var mapper = this.CreateMapper<ExtendedEntityResponse>();
            var entity = new Entity { Id = 1 };
            var response = mapper.Map(entity);
            Assert.Null(response.Embedded);
            Assert.Equal(3, response.Links.Count);
            var selfLink = response.Links[Link.Self] as Link;
            Assert.NotNull(selfLink);
            Assert.Equal($"/{entity.Id}", selfLink.Href);
        }

        private RestMapper<Entity, TResponse> CreateMapper<TResponse>()
            where TResponse : EntityResponse, new()
        {
            return new RestMapper<Entity, TResponse>(
                e => new TResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description
                },
                new ScopedStorage<IUrlHelper> { Value = new UrlHelperMock() },
                new LinkAggregator());
        }

        private class EntityResponse : RestEntity
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }
        }

        private class ExtendedEntityResponse : EntityResponse, ILinkGenerator<Entity>
        {
            public IEnumerable<NamedLink> GenerateLinks(IUrlHelper urlHelper, Entity entity)
            {
                return new[]
                {
                    new LinkToSelf(new Link($"/{entity.Id}")),
                    new NamedLink("asdf", new Link("/asdf")),
                    new NamedLink("qwer", new Link("/qwer"))
                };
            }
        }
    }
}
