// <copyright file="FilterByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class FilterByClientRequestInterpreter : IFilterByClientRequestInterpreter
    {
        public static readonly IReadOnlyDictionary<string, FilterType> FilterTypeMap =
            new Dictionary<string, FilterType>
            {
                ["~"] = FilterType.Contains,
                ["<="] = FilterType.LessThanOrEqual,
                [">="] = FilterType.GreaterThanOrEqual,
                ["<"] = FilterType.LessThan,
                [">"] = FilterType.GreaterThan,
                ["="] = FilterType.Equals,
            };

        private readonly IQueryCollection queryCollection;

        public FilterByClientRequestInterpreter(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.queryCollection = httpContextStorage.Value.Request.Query;
        }

        public IEnumerable<FilterRequest> ParseRequestQuery(
            ICollection<string> supportedFilters) =>
            this.TryParseRequestQuery(supportedFilters) ?? Enumerable.Empty<FilterRequest>();

        private IEnumerable<FilterRequest> TryParseRequestQuery(
            IEnumerable<string> supportedFilters)
        {
            return supportedFilters
                .Select(this.ResolveFilterRequest)
                .Where(f => f != null)
                .ToList();
        }

        private FilterRequest ResolveFilterRequest(string supportedFilter)
        {
            return this.queryCollection.TryGetValue(supportedFilter, out var filterValues)
                ? this.InterpretFilterRequest(supportedFilter, filterValues) : null;
        }

        private FilterRequest InterpretFilterRequest(string property, string filter)
        {
            foreach (var filterType in FilterTypeMap.Where(f => filter.StartsWith(f.Key)))
            {
                return new FilterRequest(
                    property,
                    filterType.Value,
                    filter.Substring(filterType.Key.Length));
            }

            return new FilterRequest(property, FilterType.Equals, filter);
        }
    }
}