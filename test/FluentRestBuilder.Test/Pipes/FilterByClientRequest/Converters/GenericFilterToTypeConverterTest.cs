﻿// <copyright file="GenericFilterToTypeConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Converters;
    using Xunit;

    public class GenericFilterToTypeConverterTest
    {
        private readonly GenericFilterToTypeConverter<double> provider;

        public GenericFilterToTypeConverterTest()
        {
            this.provider = new GenericFilterToTypeConverter<double>();
        }

        [Fact]
        public void TestLocalDoubleConversion()
        {
            const double value = 1.1;
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