// <copyright file="GenericFilterToTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.ComponentModel;

    public class GenericFilterToTypeConverter<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly TypeConverter converter;

        public GenericFilterToTypeConverter()
        {
            var typeConverter = TypeDescriptor.GetConverter(typeof(TFilter));
            if (typeConverter.CanConvertFrom(typeof(string)))
            {
                this.converter = typeConverter;
            }
        }

        public FilterConversionResult<TFilter> Parse(string filter)
        {
            if (this.converter == null)
            {
                return FilterConversionResult<TFilter>.CreateFailure();
            }

            return FilterConversionResult<TFilter>.CreateSuccess(
                (TFilter)this.ParseFilter(filter));
        }

        private object ParseFilter(string filter)
        {
            try
            {
                return this.converter.ConvertFromString(filter);
            }
            catch (Exception)
            {
                return this.converter.ConvertFromInvariantString(filter);
            }
        }
    }
}