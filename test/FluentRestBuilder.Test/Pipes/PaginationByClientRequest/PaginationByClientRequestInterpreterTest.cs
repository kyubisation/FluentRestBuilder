// <copyright file="PaginationByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.PaginationByClientRequest
{
    using System.Collections.Generic;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Pipes.PaginationByClientRequest;
    using FluentRestBuilder.Pipes.PaginationByClientRequest.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;
    using Storage;
    using Xunit;

    public class PaginationByClientRequestInterpreterTest
    {
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                this.CreateEmptyFilterContext(), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Page);
            Assert.Null(result.EntriesPerPage);
        }

        [Fact]
        public void TestEmptyPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                this.CreateFilterContext(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Page);
            Assert.Null(result.EntriesPerPage);
        }

        [Fact]
        public void TestEmptyEntriesPerPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                this.CreateFilterContext(entriesPerPage: string.Empty), this.keys);
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
                this.CreateFilterContext(page), this.keys);
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
                this.CreateFilterContext(entriesPerPage: entriesPerPage), this.keys);
            Assert.Throws<NotSupportedEntriesPerPageException>(() => interpreter.ParseRequestQuery());
        }

        [Fact]
        public void TestSuccessfulPageValue()
        {
            const int page = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                this.CreateFilterContext(page.ToString()), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(page, result.Page);
        }

        [Fact]
        public void TestSuccessfulEntriesPerPageValue()
        {
            const int entriesPerPage = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                this.CreateFilterContext(entriesPerPage: entriesPerPage.ToString()), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(entriesPerPage, result.EntriesPerPage);
        }

        private IScopedStorage<HttpContext> CreateEmptyFilterContext() =>
            this.CreateFilterContext(new QueryCollection());

        private IScopedStorage<HttpContext> CreateFilterContext(
            string page = null, string entriesPerPage = null)
        {
            var queryDictionary = new Dictionary<string, StringValues>();
            if (page != null)
            {
                queryDictionary[this.keys.Page] = new StringValues(page);
            }

            if (entriesPerPage != null)
            {
                queryDictionary[this.keys.EntriesPerPage] = new StringValues(entriesPerPage);
            }

            return this.CreateFilterContext(new QueryCollection(queryDictionary));
        }

        private IScopedStorage<HttpContext> CreateFilterContext(IQueryCollection collection)
        {
            return new ScopedStorage<HttpContext>
            {
                Value = new DefaultHttpContext
                {
                    Request = { Query = collection }
                }
            };
        }
    }
}
