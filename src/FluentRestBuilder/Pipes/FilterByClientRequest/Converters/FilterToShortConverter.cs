// <copyright file="FilterToShortConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToShortConverter : FilterToTypeConverterBase<short>
    {
        public FilterToShortConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out short result) =>
            short.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}