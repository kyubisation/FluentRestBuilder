// <copyright file="PaginationByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.PaginationByClientRequest
{
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Pipes.PaginationByClientRequest;
    using FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class PaginationByClientRequestInterpreterTest
    {
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new EmptyHttpContextStorage(), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Page);
            Assert.Null(result.EntriesPerPage);
        }

        [Fact]
        public void TestEmptyPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetPageValue(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Page);
            Assert.Null(result.EntriesPerPage);
        }

        [Fact]
        public void TestEmptyEntriesPerPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetEntriesPerPageValue(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Page);
            Assert.Null(result.EntriesPerPage);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("1.5")]
        [InlineData("-")]
        [InlineData("-1")]
        public void NotSupportedPageTheory(string page)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetPageValue(page), this.keys);
            Assert.Throws<NotSupportedPageException>(() => interpreter.ParseRequestQuery());
        }

        [Theory]
        [InlineData("a")]
        [InlineData("1.5")]
        [InlineData("-")]
        [InlineData("-1")]
        public void NotSupportedEntriesPerPageTheory(string entriesPerPage)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetEntriesPerPageValue(entriesPerPage), this.keys);
            Assert.Throws<NotSupportedEntriesPerPageException>(() => interpreter.ParseRequestQuery());
        }

        [Fact]
        public void TestSuccessfulPageValue()
        {
            const int page = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetPageValue(page.ToString()), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(page, result.Page);
        }

        [Fact]
        public void TestSuccessfulEntriesPerPageValue()
        {
            const int entriesPerPage = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetEntriesPerPageValue(entriesPerPage.ToString()), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(entriesPerPage, result.EntriesPerPage);
        }
    }
}
