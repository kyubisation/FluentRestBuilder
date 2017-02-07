// <copyright file="FilterToNumericTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.Globalization;

    public abstract class FilterToNumericTypeConverter<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly NumericParser<TFilter> parser;

        protected FilterToNumericTypeConverter(NumericParser<TFilter> parser)
        {
            this.parser = parser;
        }

        protected delegate bool NumericParser<TNumeric>(
            string filter, NumberStyles numberStyles, IFormatProvider formatProvider, out TNumeric result);

        public FilterConversionResult<TFilter> Parse(string filter)
        {
            TFilter result;
            return this.parser(filter, NumberStyles.Any, CultureInfo.CurrentUICulture, out result) ||
                   this.parser(filter, NumberStyles.Any, CultureInfo.InvariantCulture, out result)
                ? FilterConversionResult<TFilter>.CreateSuccess(result)
                : FilterConversionResult<TFilter>.CreateFailure();
        }
    }
}