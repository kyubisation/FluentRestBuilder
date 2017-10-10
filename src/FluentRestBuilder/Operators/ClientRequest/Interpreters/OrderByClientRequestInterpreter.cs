// <copyright file="OrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Storage;

    public class OrderByClientRequestInterpreter : IOrderByClientRequestInterpreter
    {
        private readonly IQueryCollection queryCollection;

        public OrderByClientRequestInterpreter(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.queryCollection = httpContextStorage.Value.Request.Query;
        }

        public string OrderByQueryArgumentKey { get; set; } = "sort";

        public IEnumerable<OrderByRequest> ParseRequestQuery(ICollection<string> supportedOrderBys)
        {
            if (!this.queryCollection
                .TryGetValue(this.OrderByQueryArgumentKey, out var orderByValues))
            {
                return Enumerable.Empty<OrderByRequest>();
            }

            return orderByValues
                .SelectMany(o => o.Split(','))
                .Select(o => o.Trim())
                .Where(o => !string.IsNullOrEmpty(o))
                .Select(this.ParseOrderBy)
                .Where(o => supportedOrderBys.Contains(o.Property))
                .ToList();
        }

        private OrderByRequest ParseOrderBy(string orderByString)
        {
            return orderByString.StartsWith("-")
                ? new OrderByRequest(
                    orderByString, orderByString.TrimStart('-'), OrderByDirection.Descending)
                : new OrderByRequest(
                    orderByString, orderByString.TrimStart('+'), OrderByDirection.Ascending);
        }
    }
}