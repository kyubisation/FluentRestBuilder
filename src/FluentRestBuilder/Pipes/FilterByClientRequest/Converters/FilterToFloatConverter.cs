// <copyright file="FilterToFloatConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToFloatConverter : FilterToNumericTypeConverter<float>
    {
        public FilterToFloatConverter()
            : base(float.TryParse)
        {
        }
    }
}