// <copyright file="SearchByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.SearchByClientRequest
{
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Pipes.SearchByClientRequest;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class SearchByClientRequestInterpreterTest
    {
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new SearchByClientRequestInterpreter(
                new EmptyHttpContextStorage(), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result);
        }

        [Fact]
        public void TestEmptyPageCase()
        {
            var interpreter = new SearchByClientRequestInterpreter(
                new HttpContextStorage().SetSearchValue(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result);
        }

        [Fact]
        public void TestSearchValue()
        {
            const string search = "Search";
            var interpreter = new SearchByClientRequestInterpreter(
                new HttpContextStorage().SetSearchValue(search), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(search, result);
        }
    }
}
