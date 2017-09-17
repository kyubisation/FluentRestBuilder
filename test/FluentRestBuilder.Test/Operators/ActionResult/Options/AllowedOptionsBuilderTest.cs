// <copyright file="AllowedOptionsBuilderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ActionResult.Options
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Operators.ActionResult.Options;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class AllowedOptionsBuilderTest
    {
        private readonly AllowedOptionsBuilder<Entity> builder;

        public AllowedOptionsBuilderTest()
        {
            this.builder = new AllowedOptionsBuilder<Entity>(new MockPrincipal());
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
            Assert.Single(result);
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
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(HttpVerb.Get, result);
            Assert.Contains(HttpVerb.Post, result);
        }

        [Fact]
        public void TestAllowedForAll()
        {
            var result = this.builder
                .IsAllowedForAll(e => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            var verbs = Enum.GetValues(typeof(HttpVerb))
                .Cast<HttpVerb>();
            foreach (var httpVerb in verbs)
            {
                Assert.Contains(result, v => v == httpVerb);
            }
        }

        [Fact]
        public void TestDelete()
        {
            var result = this.builder
                .IsDeleteAllowed(e => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Contains(result, v => v == HttpVerb.Delete);
        }

        [Fact]
        public void TestPatch()
        {
            var result = this.builder
                .IsPatchAllowed(e => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Contains(result, v => v == HttpVerb.Patch);
        }

        [Fact]
        public void TestPost()
        {
            var result = this.builder
                .IsPostAllowed(e => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Contains(result, v => v == HttpVerb.Post);
        }

        [Fact]
        public void TestPut()
        {
            var result = this.builder
                .IsPutAllowed(e => true)
                .GenerateAllowedVerbs(new Entity())
                .ToList();
            Assert.Contains(result, v => v == HttpVerb.Put);
        }
    }
}
