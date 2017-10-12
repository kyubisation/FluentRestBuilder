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
        [Fact]
        public void TestLocalDoubleConversion()
        {
            const double value = 1.1;
            new CultureInfo("fr-FR").AssignAsCurrentUiCulture();
            this.AssertConversion(value, value.ToString(CultureInfo.CurrentUICulture));
        }

        [Fact]
        public void TestInvariantDoubleConversion()
        {
            const double value = 1.1;
            this.AssertConversion(value, value.ToString(CultureInfo.InvariantCulture));
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertConversion<TValue>(TValue expected, string value)
        {
            var converter = new GenericFilterToTypeConverter<TValue>(
                new CultureInfoConversionPriorityCollection());
            var result = converter.Parse(value);
            Assert.True(result.Success);
            Assert.Equal(expected, result.Value);
        }
    }
}
