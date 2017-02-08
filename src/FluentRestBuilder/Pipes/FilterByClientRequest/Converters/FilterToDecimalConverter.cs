// <copyright file="FilterToDecimalConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToDecimalConverter : FilterToTypeConverterBase<decimal>
    {
        public FilterToDecimalConverter(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
            : base(cultureInfoConversionPriority)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out decimal result) =>
            decimal.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}