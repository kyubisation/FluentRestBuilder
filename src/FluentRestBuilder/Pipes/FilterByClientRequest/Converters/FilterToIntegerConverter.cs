// <copyright file="FilterToIntegerConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToIntegerConverter : FilterToTypeConverterBase<int>
    {
        public FilterToIntegerConverter(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
            : base(cultureInfoConversionPriority)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out int result) =>
            int.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}