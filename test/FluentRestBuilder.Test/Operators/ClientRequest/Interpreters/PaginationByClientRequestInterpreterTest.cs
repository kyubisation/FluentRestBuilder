// <copyright file="PaginationByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.Interpreters
{
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class PaginationByClientRequestInterpreterTest
    {
        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new EmptyHttpContextStorage());
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Fact]
        public void TestEmptyPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetOffsetValue(string.Empty));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Fact]
        public void TestEmptyEntriesPerPageCase()
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetLimitValue(string.Empty));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("1.5")]
        [InlineData("-")]
        [InlineData("-1")]
        public void NotSupportedOffsetTheory(string page)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetOffsetValue(page));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("1.5")]
        [InlineData("-")]
        [InlineData("-1")]
        public void NotSupportedLimitTheory(string entriesPerPage)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetLimitValue(entriesPerPage));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Fact]
        public void TestSuccessfulPageValue()
        {
            const int offset = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetOffsetValue(offset.ToString()));
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(offset, result.Offset);
        }

        [Fact]
        public void TestSuccessfulEntriesPerPageValue()
        {
            const int limit = 5;
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetLimitValue(limit.ToString()));
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(limit, result.Limit);
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("items=a-b")]
        [InlineData("items=7-b")]
        [InlineData("items=a-6")]
        [InlineData("items=7-")]
        [InlineData("items=-7")]
        [InlineData("items=-7-7")]
        public void RangeHeaderMalformattedTheory(string rangeValue)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetRangeHeader(rangeValue));
            var result = interpreter.ParseRequestQuery();
            Assert.Null(result.Limit);
            Assert.Null(result.Offset);
        }

        [Theory]
        [InlineData(0, 19, 20)]
        [InlineData(10, 19, 10)]
        public void RangeHeaderTheory(int rangeStart, int rangeEnd, int limit)
        {
            var interpreter = new PaginationByClientRequestInterpreter(
                new HttpContextStorage().SetRangeHeader($"items={rangeStart}-{rangeEnd}"));
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(rangeStart, result.Offset);
            Assert.Equal(limit, result.Limit);
        }

        [Fact]
        public void TestPrecedence()
        {
            const int offset = 10;
            const int limit = 10;
            var context = new HttpContextStorage()
                .SetRangeHeader($"items={0}-{29}")
                .SetOffsetValue(offset.ToString())
                .SetLimitValue(limit.ToString());
            var interpreter = new PaginationByClientRequestInterpreter(context);
            var result = interpreter.ParseRequestQuery();
            Assert.Equal(offset, result.Offset);
            Assert.Equal(limit, result.Limit);
        }
    }
}
