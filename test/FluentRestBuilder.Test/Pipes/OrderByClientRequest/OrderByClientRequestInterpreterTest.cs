// <copyright file="OrderByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Pipes.OrderByClientRequest;
    using Mocks.HttpContextStorage;
    using Xunit;

    public class OrderByClientRequestInterpreterTest
    {
        private const string Property = "Property";

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new EmptyHttpContextStorage());
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestEmptyCase()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(string.Empty));
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestAscendingOrder()
        {
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(Property));
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
            var orderByProperty = $"-{Property}";
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(orderByProperty));
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
                new OrderByRequest("-p2", "p2", OrderByDirection.Descending)
            };
            var orderBy = orderByRequests.Select(o => o.OriginalProperty)
                .Aggregate((current, next) => $"{current},{next}");
            var interpreter = new OrderByClientRequestInterpreter(
                new HttpContextStorage().SetOrderByValue(orderBy));
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(orderByRequests, result, new OrderByRequestEqualityComparer());
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
