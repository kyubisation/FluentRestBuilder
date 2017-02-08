// <copyright file="GenericFilterToTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System;
    using System.ComponentModel;

    public class GenericFilterToTypeConverter<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly ICultureInfoConversionPriority cultureInfoConversionPriority;
        private readonly TypeConverter converter;

        public GenericFilterToTypeConverter(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
        {
            this.cultureInfoConversionPriority = cultureInfoConversionPriority;
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

            var result = this.ParseFilter(filter);
            if (result == null)
            {
                return FilterConversionResult<TFilter>.CreateFailure();
            }

            return FilterConversionResult<TFilter>.CreateSuccess((TFilter)result);
        }

        private object ParseFilter(string filter)
        {
            foreach (var cultureInfo in this.cultureInfoConversionPriority)
            {
                try
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return this.converter.ConvertFromString(null, cultureInfo, filter);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return null;
        }
    }
}