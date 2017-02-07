// <copyright file="FilterToDecimalConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Converters;
    using Mocks;
    using Xunit;

    public class FilterToDecimalConverterTest
    {
        private readonly FilterToDecimalConverter converter;

        public FilterToDecimalConverterTest()
        {
            this.converter = new FilterToDecimalConverter();
            new CultureInfo("fr-FR").AssignAsCurrentUiCulture();
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1.0)]
        [InlineData("1.1", 1.1)]
        [InlineData("-1", -1.0)]
        [InlineData("-1.1", -1.1)]
        [InlineData("1,1", 1.1)]
        [InlineData("-1,1", -1.1)]
        public void TestConversion(string filter, decimal expectedResult)
        {
            var result = this.converter.Parse(filter);
            Assert.True(result.Success);
            Assert.Equal(expectedResult, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("true")]
        public void TestFailure(string filter)
        {
            var result = this.converter.Parse(filter);
            Assert.False(result.Success);
        }
    }
}
