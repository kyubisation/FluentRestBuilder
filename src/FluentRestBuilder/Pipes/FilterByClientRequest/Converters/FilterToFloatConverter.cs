// <copyright file="FilterToFloatConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;

    public class FilterToFloatConverter : FilterToTypeConverterBase<float>
    {
        public FilterToFloatConverter(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
            : base(cultureInfoConversionPriority)
        {
        }

        protected override bool TryParse(
            string filter, CultureInfo cultureInfo, out float result) =>
            float.TryParse(filter, NumberStyles.Any, cultureInfo, out result);
    }
}