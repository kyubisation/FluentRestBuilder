// <copyright file="FilterToDecimalConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToDecimalConverter : FilterToNumericTypeConverter<decimal>
    {
        public FilterToDecimalConverter()
            : base(decimal.TryParse)
        {
        }
    }
}