// <copyright file="SearchByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.Interpreters
{
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class SearchByClientRequestInterpreterTest
    {
        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new SearchByClientRequestInterpreter(new EmptyHttpContextStorage());
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result);
        }

        [Fact]
        public void TestEmptyPageCase()
        {
            var interpreter = new SearchByClientRequestInterpreter(
                new HttpContextStorage().SetSearchValue(string.Empty));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result);
        }

        [Fact]
        public void TestSearchValue()
        {
            const string search = "Search";
            var interpreter = new SearchByClientRequestInterpreter(
                new HttpContextStorage().SetSearchValue(search));
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(search, result);
        }
    }
}
