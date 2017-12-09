// <copyright file="FilterConversionResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters.Results
{
    public abstract class FilterConversionResult<TValue>
    {
        public bool Success { get; protected set; }

        public TValue Value { get; protected set; }
    }
}
