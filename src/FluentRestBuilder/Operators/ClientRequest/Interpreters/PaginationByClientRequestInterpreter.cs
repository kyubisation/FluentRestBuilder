// <copyright file="PaginationByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class PaginationByClientRequestInterpreter : IPaginationByClientRequestInterpreter
    {
        private readonly IQueryCollection queryCollection;
        private readonly IHeaderDictionary requestHeader;
        private readonly Regex rangeRegex =
            new Regex("items=(?<rangeStart>[0-9]+)-(?<rangeEnd>[0-9]+)");

        public PaginationByClientRequestInterpreter(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.queryCollection = httpContextStorage.Value.Request.Query;
            this.requestHeader = httpContextStorage.Value.Request.Headers;
        }

        public string LimitQueryArgumentKey { get; set; } = "limit";

        public string OffsetQueryArgumentKey { get; set; } = "offset";

        public PaginationRequest ParseRequestQuery() =>
            this.ParseQueryString() ?? this.ParseHeaderRange() ?? new PaginationRequest();

        private PaginationRequest ParseQueryString()
        {
            var offset = this.ParseQueryStringOffset();
            var limit = this.ParseQueryStringLimit();
            return offset == null && limit == null
                ? null
                : new PaginationRequest(offset, limit);
        }

        private int? ParseQueryStringOffset() => this.ParseQueryValue(this.OffsetQueryArgumentKey);

        private int? ParseQueryStringLimit() => this.ParseQueryValue(this.LimitQueryArgumentKey);

        private int? ParseQueryValue(string key)
        {
            if (!this.queryCollection.TryGetValue(key, out var stringValue)
                || string.IsNullOrEmpty(stringValue.ToString())
                || !int.TryParse(stringValue.ToString(), out var value)
                || value < 1)
            {
                return null;
            }

            return value;
        }

        private PaginationRequest ParseHeaderRange()
        {
            if (!this.requestHeader.TryGetValue("Range", out var rangeValue))
            {
                return null;
            }

            var match = this.rangeRegex.Match(rangeValue);
            if (!match.Success)
            {
                return null;
            }

            var rangeStart = int.Parse(match.Groups["rangeStart"].Value);
            var rangeEnd = int.Parse(match.Groups["rangeEnd"].Value);
            return rangeStart > rangeEnd
                ? null : new PaginationRequest(rangeStart, rangeEnd + 1 - rangeStart);
        }
    }
}