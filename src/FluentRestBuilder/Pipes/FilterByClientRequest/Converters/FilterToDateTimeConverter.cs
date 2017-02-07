// <copyright file="FilterToDateTimeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.Globalization;

    public class FilterToDateTimeConverter : IFilterToTypeConverter<DateTime>
    {
        public FilterConversionResult<DateTime> Parse(string filter)
        {
            DateTime result;
            return DateTime.TryParse(filter, CultureInfo.CurrentUICulture, DateTimeStyles.None, out result) ||
                   DateTime.TryParse(filter, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
                ? FilterConversionResult<DateTime>.CreateSuccess(result)
                : FilterConversionResult<DateTime>.CreateFailure();
        }
    }
}