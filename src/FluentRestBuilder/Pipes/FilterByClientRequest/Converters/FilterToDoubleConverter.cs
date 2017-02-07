// <copyright file="FilterToDoubleConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToDoubleConverter : FilterToNumericTypeConverter<double>
    {
        public FilterToDoubleConverter()
            : base(double.TryParse)
        {
        }
    }
}