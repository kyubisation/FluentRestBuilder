// <copyright file="FilterConversionResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterConversionResult<TValue>
    {
        public bool Success { get; set; }

        public TValue Value { get; set; }

        public static FilterConversionResult<TValue> CreateFailure() =>
            new FilterConversionResult<TValue>();

        public static FilterConversionResult<TValue> CreateSuccess(TValue value) =>
            new FilterConversionResult<TValue> { Success = true, Value = value };
    }
}
