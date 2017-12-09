// <copyright file="FilterToFloatConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;

    public class FilterToFloatConverter : FilterToTypeConverterBase<float>
    {
        public FilterToFloatConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
            : base(cultureInfoConversionPriorityCollection)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out float result) =>
            float.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}