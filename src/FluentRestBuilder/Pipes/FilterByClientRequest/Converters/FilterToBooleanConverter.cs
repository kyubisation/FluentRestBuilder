﻿// <copyright file="FilterToBooleanConverter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Converters
{
    using Results;

    public class FilterToBooleanConverter : IFilterToTypeConverter<bool>
    {
        public FilterConversionResult<bool> Parse(string filter)
        {
            bool result;
            if (bool.TryParse(filter, out result) || TryParse(filter, out result))
            {
                return new FilterConversionSuccess<bool>(result);
            }

            return new FilterConversionFailure<bool>();
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