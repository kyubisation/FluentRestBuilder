// <copyright file="GenericFilterToTypeConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;
    using FluentRestBuilder.Operators.ClientRequest.FilterConverters;
    using Mocks;
    using Xunit;

    public class GenericFilterToTypeConverterTest
    {
        private readonly GenericFilterToTypeConverter<double> provider;

        public GenericFilterToTypeConverterTest()
        {
            this.provider = new GenericFilterToTypeConverter<double>(
                new CultureInfoConversionPriorityCollection());
        }

        [Fact]
        public void TestLocalDoubleConversion()
        {
            const double value = 1.1;
            new CultureInfo("fr-FR").AssignAsCurrentUiCulture();
            var result = this.provider.Parse(value.ToString(CultureInfo.CurrentUICulture));
            Assert.True(result.Success);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void TestInvariantDoubleConversion()
        {
            const double value = 1.1;
            var result = this.provider.Parse(value.ToString(CultureInfo.InvariantCulture));
            Assert.True(result.Success);
            Assert.Equal(value, result.Value);
        }
    }
}
