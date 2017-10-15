// <copyright file="FilterToIntegerConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;

    public class FilterToIntegerConverter : FilterToTypeConverterBase<int>
    {
        public FilterToIntegerConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out int result) =>
            int.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}