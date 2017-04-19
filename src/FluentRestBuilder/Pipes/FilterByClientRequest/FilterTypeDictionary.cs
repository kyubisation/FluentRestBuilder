// <copyright file="FilterTypeDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FilterTypeDictionary : ReadOnlyDictionary<string, FilterType>
    {
        public FilterTypeDictionary()
            : base(CreateMapping())
        {
        }

        private static IDictionary<string, FilterType> CreateMapping()
        {
            return new Dictionary<string, FilterType>
            {
                ["~"] = FilterType.Contains,
                ["<="] = FilterType.LessThanOrEqual,
                [">="] = FilterType.GreaterThanOrEqual,
                ["<"] = FilterType.LessThan,
                [">"] = FilterType.GreaterThan,
                ["="] = FilterType.Equals,
            };
        }
    }
}
