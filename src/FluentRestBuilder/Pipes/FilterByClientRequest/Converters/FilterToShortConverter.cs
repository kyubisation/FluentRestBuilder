// <copyright file="FilterToShortConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToShortConverter : FilterToNumericTypeConverter<short>
    {
        public FilterToShortConverter()
            : base(short.TryParse)
        {
        }
    }
}