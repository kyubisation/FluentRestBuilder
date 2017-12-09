// <copyright file="FilterToBooleanConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterConverters
{
    using FluentRestBuilder.Operators.ClientRequest.FilterConverters;
    using Xunit;

    public class FilterToBooleanConverterTest
    {
        private readonly FilterToBooleanConverter converter;

        public FilterToBooleanConverterTest()
        {
            this.converter = new FilterToBooleanConverter();
        }

        [Theory]
        [InlineData("True", true)]
        [InlineData("true", true)]
        [InlineData("1", true)]
        [InlineData("False", false)]
        [InlineData("false", false)]
        [InlineData("0", false)]
        public void TestConversion(string filter, bool expectedResult)
        {
            var result = this.converter.Parse(filter);
            Assert.True(result.Success);
            Assert.Equal(expectedResult, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("1.1")]
        [InlineData("-1")]
        public void TestFailure(string filter)
        {
            var result = this.converter.Parse(filter);
            Assert.False(result.Success);
        }
    }
}
