// <copyright file="FilterConversionSuccess.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters.Results
{
    public class FilterConversionSuccess<TValue> : FilterConversionResult<TValue>
    {
        public FilterConversionSuccess(TValue value)
        {
            this.Value = value;
            this.Success = true;
        }
    }
}
