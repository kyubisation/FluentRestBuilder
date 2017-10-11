// <copyright file="FilterByClientRequestInterpreterTest.cs" company="Kyubisation">
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

    public class FilterByClientRequestInterpreterTest
    {
        private const string Property = "Property";
        private const string Filter = "Filter";

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                new EmptyHttpContextStorage(), new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { "p1", "p2" });
            Assert.Empty(result);
        }

        [Fact]
        public void TestEmptyCase()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                new HttpContextStorage(), new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { "p1", "p2" });
            Assert.Empty(result);
        }

        [Fact]
        public void TestEquals()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                new HttpContextStorage().SetValue(Property.ToLower(), Filter),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { Property }).ToList();
            Assert.Single(result);
            var request = result.First();
            Assert.Equal(FilterType.Equals, request.FilterType);
            Assert.Equal(Property, request.Property);
            Assert.Equal(Filter, request.Filter);
        }

        [Theory]
        [InlineData("~", FilterType.Contains)]
        [InlineData("<=", FilterType.LessThanOrEqual)]
        [InlineData(">=", FilterType.GreaterThanOrEqual)]
        [InlineData("<", FilterType.LessThan)]
        [InlineData(">", FilterType.GreaterThan)]
        public void TestFilterTheory(string prefix, FilterType type)
        {
            this.TestSingleFilterCase(prefix, type);
        }

        [Fact]
        public void TestMultipleCases()
        {
            var filterRequests = new List<FilterRequest>
            {
                new FilterRequest("p1", FilterType.Equals, "p1"),
                new FilterRequest("p3", FilterType.Contains, "p3"),
                new FilterRequest("p4", FilterType.LessThan, "p4"),
                new FilterRequest("p5", FilterType.GreaterThan, "p5"),
                new FilterRequest("p6", FilterType.LessThanOrEqual, "p6"),
                new FilterRequest("p7", FilterType.GreaterThanOrEqual, "p7"),
            };
            var context = new HttpContextStorage();
            foreach (var filterRequest in filterRequests)
            {
                var prefix = FilterByClientRequestInterpreter.FilterTypeMap
                    .Where(p => p.Value == filterRequest.FilterType)
                    .Select(p => p.Key)
                    .First();
                context.SetValue(filterRequest.Property, $"{prefix}{filterRequest.Filter}");
            }

            var interpreter = new FilterByClientRequestInterpreter(
                context, new MockPropertyNameResolver());
            var result = interpreter
                .ParseRequestQuery(filterRequests.Select(r => r.Property).ToList())
                .ToList();
            foreach (var filterRequest in filterRequests)
            {
                Assert.Contains(filterRequest, result, new PropertyComparer<FilterRequest>());
            }
        }

        // ReSharper disable once UnusedParameter.Local
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void TestSingleFilterCase(string filterPrefix, FilterType expectedType)
        {
            var interpreter = new FilterByClientRequestInterpreter(
                new HttpContextStorage().SetValue(Property.ToLower(), $"{filterPrefix}{Filter}"),
                new MockPropertyNameResolver());
            var result = interpreter.ParseRequestQuery(new[] { Property }).ToList();
            Assert.Single(result);
            var request = result.First();
            Assert.Equal(expectedType, request.FilterType);
            Assert.Equal(Property, request.Property);
            Assert.Equal(Filter, request.Filter);
        }
    }
}
