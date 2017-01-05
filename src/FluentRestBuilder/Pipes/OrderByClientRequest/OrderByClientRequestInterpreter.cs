// <copyright file="OrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Storage;

    public class OrderByClientRequestInterpreter : IOrderByClientRequestInterpreter
    {
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public OrderByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryArgumentKeys = queryArgumentKeys;
            this.queryCollection = httpContextStorage.Value.Request.Query;
        }

        public IEnumerable<OrderByRequest> ParseRequestQuery()
        {
            StringValues orderByValues;
            if (!this.queryCollection.TryGetValue(this.queryArgumentKeys.OrderBy, out orderByValues))
            {
                return Enumerable.Empty<OrderByRequest>();
            }

            return orderByValues
                .SelectMany(o => o.Split(','))
                .Select(o => o.Trim())
                .Where(o => !string.IsNullOrEmpty(o))
                .Select(this.ParseOrderBy)
                .ToList();
        }

        private OrderByRequest ParseOrderBy(string orderByString)
        {
            return orderByString.StartsWith("!")
                ? new OrderByRequest(
                    orderByString, orderByString.TrimStart('!', ' '), OrderByDirection.Descending)
                : new OrderByRequest(orderByString, OrderByDirection.Ascending);
        }
    }
}