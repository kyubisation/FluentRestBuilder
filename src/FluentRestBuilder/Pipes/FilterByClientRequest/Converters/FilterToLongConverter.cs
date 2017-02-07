// <copyright file="FilterToLongConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToLongConverter : FilterToNumericTypeConverter<long>
    {
        public FilterToLongConverter()
            : base(long.TryParse)
        {
        }
    }
}