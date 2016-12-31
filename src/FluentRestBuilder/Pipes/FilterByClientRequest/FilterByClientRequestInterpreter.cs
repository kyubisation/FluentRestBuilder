// <copyright file="FilterByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using Storage;

    public class FilterByClientRequestInterpreter : IFilterByClientRequestInterpreter
    {
        private static readonly IDictionary<string, FilterType> TypeMap = new Dictionary<string, FilterType>
        {
            ["~"] = FilterType.Contains,
            ["<="] = FilterType.LessThanOrEqual,
            [">="] = FilterType.GreaterThanOrEqual,
            ["<"] = FilterType.LessThan,
            [">"] = FilterType.GreaterThan
        };

        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public FilterByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryArgumentKeys = queryArgumentKeys;
            this.queryCollection = httpContextStorage.Value.Request.Query;
        }

        public IEnumerable<FilterRequest> ParseRequestQuery()
        {
            StringValues filterValues;
            return !this.queryCollection.TryGetValue(this.queryArgumentKeys.Filter, out filterValues)
                ? Enumerable.Empty<FilterRequest>()
                : this.DeserializeFiltersAndCreateFilterRequests(filterValues);
        }

        private IEnumerable<FilterRequest> DeserializeFiltersAndCreateFilterRequests(StringValues filterValues)
        {
            return filterValues.ToArray()
                .SelectMany(this.DeserializeFilter)
                .Select(f => this.InterpretFilterRequest(f.Key, f.Value));
        }

        private IDictionary<string, string> DeserializeFilter(string filter)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(filter);
            }
            catch (JsonSerializationException)
            {
                throw new FilterInterpreterException(filter);
            }
        }

        private FilterRequest InterpretFilterRequest(string property, string filter)
        {
            foreach (var filterType in TypeMap.Where(f => property.EndsWith(f.Key)))
            {
                return new FilterRequest(
                    property,
                    property.TrimEnd($"{filterType.Key} ".ToCharArray()),
                    filterType.Value,
                    filter);
            }

            return new FilterRequest(property, FilterType.Equals, filter);
        }
    }
}