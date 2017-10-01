// <copyright file="FilterToTypeConverterBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;
    using System.Linq;
    using Results;

    public abstract class FilterToTypeConverterBase<TFilter> : IFilterToTypeConverter<TFilter>
    {
        private readonly ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection;

        protected FilterToTypeConverterBase(
            ICultureInfoConversionPriorityCollection cultureInfoConversionPriorityCollection)
        {
            this.cultureInfoConversionPriorityCollection = cultureInfoConversionPriorityCollection;
        }

        public FilterConversionResult<TFilter> Parse(string filter)
        {
            var result = default(TFilter);
            if (this.cultureInfoConversionPriorityCollection
                .Any(cultureInfo => this.TryParse(filter, cultureInfo, out result)))
            {
                return new FilterConversionSuccess<TFilter>(result);
            }

            return new FilterConversionFailure<TFilter>();
        }

        protected abstract bool TryParse(string filter, CultureInfo cultureInfo, out TFilter result);
    }
}
