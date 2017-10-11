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
        private readonly IJsonPropertyNameResolver jsonPropertyNameResolver;

        public FilterByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IJsonPropertyNameResolver jsonPropertyNameResolver)
        {
            this.jsonPropertyNameResolver = jsonPropertyNameResolver;
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
            var filterKey = this.jsonPropertyNameResolver.Resolve(supportedFilter);
            return this.queryCollection.TryGetValue(filterKey, out var filterValues)
                ? this.InterpretFilterRequest(filterKey, supportedFilter, filterValues) : null;
        }

        private FilterRequest InterpretFilterRequest(
            string originalProperty, string property, string filter)
        {
            foreach (var filterType in FilterTypeMap.Where(f => filter.StartsWith(f.Key)))
            {
                return new FilterRequest(
                    originalProperty,
                    property,
                    filterType.Value,
                    filter.Substring(filterType.Key.Length));
            }

            return new FilterRequest(originalProperty, property, FilterType.Equals, filter);
        }
    }
}