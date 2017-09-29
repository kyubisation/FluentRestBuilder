// <copyright file="FilterToDecimalConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;

    public class FilterToDecimalConverter : FilterToTypeConverterBase<decimal>
    {
        public FilterToDecimalConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out decimal result) =>
            decimal.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}