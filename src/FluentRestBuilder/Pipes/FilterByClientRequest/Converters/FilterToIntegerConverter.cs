// <copyright file="FilterToIntegerConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToIntegerConverter : FilterToNumericTypeConverter<int>
    {
        public FilterToIntegerConverter()
            : base(int.TryParse)
        {
        }
    }
}