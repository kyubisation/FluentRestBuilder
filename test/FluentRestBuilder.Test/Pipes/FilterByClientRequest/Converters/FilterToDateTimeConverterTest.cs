// <copyright file="FilterToDateTimeConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.Globalization;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Converters;
    using Mocks;
    using Xunit;

    public class FilterToDateTimeConverterTest
    {
        private readonly FilterToDateTimeConverter converter;

        public FilterToDateTimeConverterTest()
        {
            this.converter = new FilterToDateTimeConverter(new CultureInfoConversionPriority());
        }

        [Fact]
        public void TestInvariantFullDateTimeString()
        {
            new CultureInfo("de-CH").AssignAsCurrentUiCulture();
            var date = new DateTime(2017, 1, 31).ToString(CultureInfo.InvariantCulture);
            var result = this.converter.Parse(date);
            Assert.True(result.Success);
            Assert.Equal(date, result.Value.ToString(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void TestLocalFullDateTimeString()
        {
            var date = DateTime.UtcNow.ToString(CultureInfo.CurrentUICulture);
            var result = this.converter.Parse(date);
            Assert.True(result.Success);
            Assert.Equal(date, result.Value.ToString(CultureInfo.CurrentUICulture));
        }

        [Fact]
        public void TestDateString()
        {
            var date = DateTime.UtcNow.Date;
            var dateString = date.ToString("d", CultureInfo.InvariantCulture);
            var result = this.converter.Parse(dateString);
            Assert.True(result.Success);
            Assert.Equal(
                date.ToString(CultureInfo.InvariantCulture),
                result.Value.ToString(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void TestLocalDateString()
        {
            var cultureInfo = new CultureInfo("de-CH").AssignAsCurrentUiCulture();
            var date = DateTime.UtcNow.Date;
            var dateString = date.ToString("d", cultureInfo);
            var result = this.converter.Parse(dateString);
            Assert.True(result.Success);
            Assert.Equal(
                date.ToString(CultureInfo.InvariantCulture),
                result.Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
