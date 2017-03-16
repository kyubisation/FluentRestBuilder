// <copyright file="FilterToLongConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToLongConverter : FilterToTypeConverterBase<long>
    {
        public FilterToLongConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out long result) =>
            long.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}