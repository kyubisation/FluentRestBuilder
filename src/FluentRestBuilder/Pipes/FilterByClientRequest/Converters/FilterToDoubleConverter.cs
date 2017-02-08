// <copyright file="FilterToDoubleConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToDoubleConverter : FilterToTypeConverterBase<double>
    {
        public FilterToDoubleConverter(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
            : base(cultureInfoConversionPriority)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out double result) =>
            double.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}