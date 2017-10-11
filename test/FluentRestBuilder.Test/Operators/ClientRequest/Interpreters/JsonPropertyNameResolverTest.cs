// <copyright file="JsonPropertyNameResolverTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.Interpreters
{
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Xunit;

    public class JsonPropertyNameResolverTest
    {
        [Theory]
        [InlineData("Test", "test")]
        [InlineData("Q", "q")]
        public void TestPropertyResolving(string value, string expected)
        {
            var options = new OptionsWrapper<MvcJsonOptions>(new MvcJsonOptions());
            var resolver = new JsonPropertyNameResolver(options);
            var resolvedValue = resolver.Resolve(value);
            Assert.Equal(expected, resolvedValue);
        }
    }
}
