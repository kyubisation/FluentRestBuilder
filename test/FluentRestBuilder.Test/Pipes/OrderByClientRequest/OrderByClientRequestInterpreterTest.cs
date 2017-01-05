// <copyright file="OrderByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Pipes.OrderByClientRequest;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;
    using Storage;
    using Xunit;

    public class OrderByClientRequestInterpreterTest
    {
        private const string Property = "Property";
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                this.CreateEmptyFilterContext(), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestEmptyCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                this.CreateFilterContext(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestAscendingOrder()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                this.CreateFilterContext(Property), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(1, result.Count);
            var request = result.First();
            Assert.Equal(Property, request.Property);
            Assert.Equal(Property, request.OriginalProperty);
            Assert.Equal(OrderByDirection.Ascending, request.Direction);
        }

        [Fact]
        public void TestDescendingOrder()
        {
            var orderByProperty = $"!{Property}";
            var interpreter = new OrderByClientRequestInterpreter(
                this.CreateFilterContext(orderByProperty), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(1, result.Count);
            var request = result.First();
            Assert.Equal(Property, request.Property);
            Assert.Equal(orderByProperty, request.OriginalProperty);
            Assert.Equal(OrderByDirection.Descending, request.Direction);
        }

        [Fact]
        public void TestMultipleOrderBys()
        {
            var orderByRequests = new List<OrderByRequest>
            {
                new OrderByRequest("p1", OrderByDirection.Ascending),
                new OrderByRequest("!p2", "p2", OrderByDirection.Descending)
            };
            var orderBy = orderByRequests.Select(o => o.OriginalProperty)
                .Aggregate((current, next) => $"{current},{next}");
            var interpreter = new OrderByClientRequestInterpreter(
                this.CreateFilterContext(orderBy), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(orderByRequests, result, new OrderByRequestEqualityComparer());
        }

        private IScopedStorage<HttpContext> CreateEmptyFilterContext() =>
            this.CreateFilterContext(new QueryCollection());

        private IScopedStorage<HttpContext> CreateFilterContext(string value)
        {
            return this.CreateFilterContext(new QueryCollection(new Dictionary<string, StringValues>
            {
                [this.keys.OrderBy] = new StringValues(value)
            }));
        }

        private IScopedStorage<HttpContext> CreateFilterContext(IQueryCollection collection)
        {
            return new ScopedStorage<HttpContext>
            {
                Value = new DefaultHttpContext
                {
                    Request = { Query = collection }
                }
            };
        }

        private class OrderByRequestEqualityComparer : EqualityComparer<OrderByRequest>
        {
            public override bool Equals(OrderByRequest x, OrderByRequest y) =>
                x.OriginalProperty == y.OriginalProperty
                    && x.Property == y.Property
                //// ReSharper disable once EqualExpressionComparison
                    && x.Direction == x.Direction;

            public override int GetHashCode(OrderByRequest obj) =>
                $"{obj.OriginalProperty}:{obj.Property}:{obj.Direction}".GetHashCode();
        }
    }
}
