// <copyright file="FilterToTypeConverterBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using System.Globalization;
    using System.Linq;

    public abstract class FilterToTypeConverterBase<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly ICultureInfoConversionPriority cultureInfoConversionPriority;

        protected FilterToTypeConverterBase(
            ICultureInfoConversionPriority cultureInfoConversionPriority)
        {
            this.cultureInfoConversionPriority = cultureInfoConversionPriority;
        }

        public FilterConversionResult<TFilter> Parse(string filter)
        {
            var result = default(TFilter);
            if (this.cultureInfoConversionPriority
                .Any(cultureInfo => this.TryParse(filter, cultureInfo, out result)))
            {
                return FilterConversionResult<TFilter>.CreateSuccess(result);
            }

            return FilterConversionResult<TFilter>.CreateFailure();
        }

        protected abstract bool TryParse(string filter, CultureInfo cultureInfo, out TFilter result);
    }
}
