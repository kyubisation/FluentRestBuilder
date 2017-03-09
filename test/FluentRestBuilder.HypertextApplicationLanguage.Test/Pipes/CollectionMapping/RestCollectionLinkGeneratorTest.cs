// <copyright file="RestCollectionLinkGeneratorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Pipes.CollectionMapping
{
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Microsoft.AspNetCore.Http;
    using Xunit;

    public class RestCollectionLinkGeneratorTest
    {
        [Fact]
        public void TestEmpty()
        {
            var generator = this.Create();
            var result = generator.GenerateLinks(null)
                .ToList();
            Assert.Equal(1, result.Count);
            var first = result.First();
            Assert.IsType<LinkToSelf>(first);
        }

        [Fact]
        public void TestSinglePage()
        {
            var generator = this.Create();
            var result = generator.GenerateLinks(new PaginationMetaInfo(1, 0, 1))
                .ToList();
            Assert.Equal(1, result.Count);
            var first = result.First();
            Assert.IsType<LinkToSelf>(first);
        }

        [Fact]
        public void TestFirstPage()
        {
            var generator = this.Create();
            var result = generator.GenerateLinks(new PaginationMetaInfo(2, 0, 1))
                .ToList();
            Assert.Equal(3, result.Count);
            Assert.Contains(Link.Self, result.Select(l => l.Name));
            Assert.Contains("next", result.Select(l => l.Name));
            Assert.Contains("last", result.Select(l => l.Name));
        }

        [Fact]
        public void TestSecondPage()
        {
            var generator = this.Create();
            var result = generator.GenerateLinks(new PaginationMetaInfo(2, 2, 1))
                .ToList();
            Assert.Equal(3, result.Count);
            Assert.Contains(Link.Self, result.Select(l => l.Name));
            Assert.Contains("first", result.Select(l => l.Name));
            Assert.Contains("previous", result.Select(l => l.Name));
        }

        [Fact]
        public void TestMultiplePages()
        {
            var generator = this.Create();
            var result = generator.GenerateLinks(new PaginationMetaInfo(3, 1, 1))
                .ToList();
            Assert.Equal(5, result.Count);
            Assert.Contains(Link.Self, result.Select(l => l.Name));
            Assert.Contains("next", result.Select(l => l.Name));
            Assert.Contains("last", result.Select(l => l.Name));
            Assert.Contains("first", result.Select(l => l.Name));
            Assert.Contains("previous", result.Select(l => l.Name));
        }

        private RestCollectionLinkGenerator Create()
        {
            return new RestCollectionLinkGenerator(
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
                            Scheme = "http"
                        }
                    }
                });
        }
    }
}
