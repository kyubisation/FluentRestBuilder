// <copyright file="AllowedOptionsBuilderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Results.Options
{
    using System.Linq;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Results.Options;
    using Microsoft.AspNetCore.Http;
    using Storage;
    using Xunit;

    public class AllowedOptionsBuilderTest
    {
        private readonly AllowedOptionsBuilder<Entity> builder;

        public AllowedOptionsBuilderTest()
        {
            var httpContextStorage = new ScopedStorage<HttpContext>
            {
                Value = new DefaultHttpContext
                {
                    User = new MockPrincipal()
                }
            };
            this.builder = new AllowedOptionsBuilder<Entity>(httpContextStorage);
        }

        [Fact]
        public void TestEmptyUseCase()
        {
            var result = this.builder.GenerateAllowedVerbs(new Entity());
            Assert.Empty(result);
        }

        [Fact]
        public void TestSingleChecks()
        {
            var result = this.builder.IsGetAllowed((p, e) => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Equal(1, result.Count);
            Assert.Contains(HttpVerb.Get, result);
        }

        [Fact]
        public void TestMultipleChecks()
        {
            var result = this.builder.IsGetAllowed((p, e) => true)
                .IsPostAllowed((p, e) => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(HttpVerb.Get, result);
            Assert.Contains(HttpVerb.Post, result);
        }

        [Fact]
        public void TestContradictingChecks()
        {
            var result = this.builder
                .IsAllowed(new[] { HttpVerb.Get, HttpVerb.Delete }, (p, e) => false)
                .IsAllowed(new[] { HttpVerb.Get, HttpVerb.Post }, (p, e) => true)
                .GenerateAllowedVerbs(new Entity()).ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(HttpVerb.Get, result);
            Assert.Contains(HttpVerb.Post, result);
        }
    }
}
