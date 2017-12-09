// <copyright file="GenericFilterToTypeConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using System;
    using System.ComponentModel;
    using Results;

    public class GenericFilterToTypeConverter<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection;
        private readonly TypeConverter converter;

        public GenericFilterToTypeConverter(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
        {
            this.cultureInfoConversionPriorityCollection = cultureInfoConversionPriorityCollection;
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
                return new FilterConversionFailure<TFilter>();
            }

            var result = this.ParseFilter(filter);
            if (result == null)
            {
                return new FilterConversionFailure<TFilter>();
            }

            return new FilterConversionSuccess<TFilter>((TFilter)result);
        }

        private object ParseFilter(string filter)
        {
            foreach (var cultureInfo in this.cultureInfoConversionPriorityCollection)
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