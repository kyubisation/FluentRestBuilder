// <copyright file="IFilterToTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using Results;

    public interface IFilterToTypeConverter<TFilter>
    {
        FilterConversionResult<TFilter> Parse(string filter);
    }
}
