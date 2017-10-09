// <copyright file="RestCollectionGeneratorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Operators
{
    using System.Linq;
    using FluentRestBuilder.Mocks.EntityFramework;
    using FluentRestBuilder.Operators.ClientRequest;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Operators;
    using Microsoft.AspNetCore.Http;
    using Mocks;
    using Xunit;

    public class RestCollectionGeneratorTest
    {
        [Fact]
        public void TestEmpty()
        {
            var result = this.Create()
                .CreateCollection(Enumerable.Empty<Entity>(), e => new EntityResponse(e));
            Assert.Single(result._links);
            Assert.Contains(Link.Self, result._links.Keys);
        }

        [Fact]
        public void TestSinglePage()
        {
            var result = this.Create()
                .CreateCollection(
                Enumerable.Empty<Entity>(),
                e => new EntityResponse(e),
                new PaginationInfo(1, 0, 1));
            Assert.Single(result._links);
            Assert.Contains(Link.Self, result._links.Keys);
        }

        [Fact]
        public void TestFirstPage()
        {
            var result = this.Create()
                .CreateCollection(
                    Enumerable.Empty<Entity>(),
                    e => new EntityResponse(e),
                    new PaginationInfo(2, 0, 1));
            Assert.Equal(3, result._links.Count);
            Assert.Contains(Link.Self, result._links.Keys);
            Assert.Contains("next", result._links.Keys);
            Assert.Contains("last", result._links.Keys);
        }

        [Fact]
        public void TestSecondPage()
        {
            var result = this.Create()
                .CreateCollection(
                    Enumerable.Empty<Entity>(),
                    e => new EntityResponse(e),
                    new PaginationInfo(2, 2, 1));
            Assert.Equal(3, result._links.Count);
            Assert.Contains(Link.Self, result._links.Keys);
            Assert.Contains("first", result._links.Keys);
            Assert.Contains("previous", result._links.Keys);
        }

        [Fact]
        public void TestMultiplePages()
        {
            var result = this.Create()
                .CreateCollection(
                    Enumerable.Empty<Entity>(),
                    e => new EntityResponse(e),
                    new PaginationInfo(3, 1, 1));
            Assert.Equal(5, result._links.Count);
            Assert.Contains(Link.Self, result._links.Keys);
            Assert.Contains("next", result._links.Keys);
            Assert.Contains("last", result._links.Keys);
            Assert.Contains("first", result._links.Keys);
            Assert.Contains("previous", result._links.Keys);
        }

        private RestCollectionGenerator<Entity, EntityResponse> Create()
        {
            return new RestCollectionGenerator<Entity, EntityResponse>(
                new LinkHelper(
                    new ScopedStorage<HttpContext>
                    {
                        Value = new DefaultHttpContext
                        {
                            Request =
                            {
                                Host = HostString.FromUriComponent("test.com"),
                                PathBase = PathString.Empty,
                                Path = PathString.Empty,
                                QueryString = QueryString.Empty,
                                Scheme = "http",
                            },
                        },
                    }));
        }
    }
}
