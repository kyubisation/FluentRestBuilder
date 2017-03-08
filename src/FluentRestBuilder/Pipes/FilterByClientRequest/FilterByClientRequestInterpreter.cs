// <copyright file="FilterByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Storage;

    public class FilterByClientRequestInterpreter : IFilterByClientRequestInterpreter
    {
        private readonly IReadOnlyDictionary<string, FilterType> typeMap;

        private readonly IQueryCollection queryCollection;

        public FilterByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IReadOnlyDictionary<string, FilterType> typeMap)
        {
            this.queryCollection = httpContextStorage.Value.Request.Query;
            this.typeMap = typeMap;
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
            StringValues filterValues;
            return this.queryCollection.TryGetValue(supportedFilter, out filterValues)
                ? this.InterpretFilterRequest(supportedFilter, filterValues) : null;
        }

        private FilterRequest InterpretFilterRequest(string property, string filter)
        {
            foreach (var filterType in this.typeMap.Where(f => filter.StartsWith(f.Key)))
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