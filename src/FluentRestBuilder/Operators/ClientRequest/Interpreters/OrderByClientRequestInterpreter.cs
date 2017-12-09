// <copyright file="OrderByClientRequestInterpreter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using System.Linq;
    using Json;
    using Microsoft.AspNetCore.Http;
    using Requests;
    using Storage;

    public class OrderByClientRequestInterpreter : IOrderByClientRequestInterpreter
    {
        private readonly IJsonPropertyNameResolver jsonPropertyNameResolver;
        private readonly IQueryCollection queryCollection;

        public OrderByClientRequestInterpreter(
            IScopedStorage<HttpContext> httpContextStorage,
            IJsonPropertyNameResolver jsonPropertyNameResolver)
        {
            this.jsonPropertyNameResolver = jsonPropertyNameResolver;
            this.queryCollection = httpContextStorage.Value.Request.Query;
            this.OrderByQueryArgumentKey = this.jsonPropertyNameResolver.Resolve("Sort");
        }

        public string OrderByQueryArgumentKey { get; set; }

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
                .Select(o => this.ParseOrderBy(o, supportedOrderBys))
                .Where(o => o != null)
                .ToList();
        }

        private OrderByRequest ParseOrderBy(string orderByString, IEnumerable<string> supportedOrderBys)
        {
            var direction = orderByString.StartsWith("-")
                ? OrderByDirection.Descending
                : OrderByDirection.Ascending;
            var property = orderByString.TrimStart('-', '+');
            return (
                from supportedOrderBy
                in supportedOrderBys
                where this.jsonPropertyNameResolver.Resolve(supportedOrderBy) == property
                select new OrderByRequest(orderByString, supportedOrderBy, direction))
                .FirstOrDefault();
        }
    }
}