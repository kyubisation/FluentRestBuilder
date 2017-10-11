// <copyright file="OrderByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.Interpreters
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Mocks;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class OrderByClientRequestInterpreterTest
    {
        private const string Property = "Property";

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new EmptyHttpContextStorage(), new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { "p1" });
            Assert.Empty(result);
        }

        [Fact]
        public void TestEmptyCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(string.Empty),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { "p1" });
            Assert.Empty(result);
        }

        [Fact]
        public void TestAscendingOrder()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(Property.ToLower()),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { Property })
                .ToList();
            Assert.Single(result);
            var request = result.First();
            Assert.Equal(Property, request.Property);
            Assert.Equal(Property.ToLower(), request.OriginalProperty);
            Assert.Equal(OrderByDirection.Ascending, request.Direction);
        }

        [Fact]
        public void TestDescendingOrder()
        {
            var orderByProperty = $"-{Property}".ToLower();
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(orderByProperty),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { Property })
                .ToList();
            Assert.Single(result);
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
                new OrderByRequest("-p2", "p2", OrderByDirection.Descending),
            };
            var orderBy = orderByRequests.Select(o => o.OriginalProperty)
                .Aggregate((current, next) => $"{current},{next}");
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(orderBy),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { "p1", "p2" })
                .ToList();
            Assert.Equal(orderByRequests, result, new PropertyComparer<OrderByRequest>());
        }
    }
}
