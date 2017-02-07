// <copyright file="FilterToBooleanConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    public class FilterToBooleanConverter : IFilterToTypeConverter<bool>
    {
        public FilterConversionResult<bool> Parse(string filter)
        {
            bool result;
            return bool.TryParse(filter, out result) || TryParse(filter, out result)
                ? FilterConversionResult<bool>.CreateSuccess(result)
                : FilterConversionResult<bool>.CreateFailure();
        }

        private static bool TryParse(string filter, out bool result)
        {
            result = false;
            switch (filter.Trim())
            {
                case "0":
                    return true;
                case "1":
                    result = true;
                    return true;
                default:
                    return false;
            }
        }
    }
}