// <copyright file="OrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public class OrderByClientRequestInterpreter : IOrderByClientRequestInterpreter
    {
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public OrderByClientRequestInterpreter(
            IHttpContextAccessor httpContextAccessor,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryArgumentKeys = queryArgumentKeys;
            this.queryCollection = httpContextAccessor.HttpContext.Request.Query;
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
                .Select(this.ParseOrderBy);
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