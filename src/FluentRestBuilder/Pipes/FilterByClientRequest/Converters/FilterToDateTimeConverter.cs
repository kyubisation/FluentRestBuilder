// <copyright file="FilterToDateTimeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.Globalization;

    public class FilterToDateTimeConverter : FilterToTypeConverterBase<DateTime>
    {
        public FilterToDateTimeConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out DateTime result) =>
            DateTime.TryParse(filter, cultureInfo, DateTimeStyles.None, out result);
    }
}