// <copyright file="FilterByClientRequestInterpreterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Pipes.FilterByClientRequest;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using Storage;
    using Xunit;

    public class FilterByClientRequestInterpreterTest
    {
        private const string Property = "Property";
        private const string Filter = "Filter";
        private readonly IQueryArgumentKeys keys = new QueryArgumentKeys();

        [Fact]
        public void TestNonExistantCase()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateEmptyFilterContext(), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestEmptyCase()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateFilterContext(string.Empty), this.keys);
            var result = interpreter.ParseRequestQuery();
            Assert.Empty(result);
        }

        [Fact]
        public void TestEquals()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateFilterContext(Property, Filter), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(1, result.Count);
            var request = result.First();
            Assert.Equal(FilterType.Equals, request.Type);
            Assert.Equal(Property, request.Property);
            Assert.Equal(Property, request.OriginalProperty);
            Assert.Equal(Filter, request.Filter);
        }

        [Theory]
        [InlineData("~", FilterType.Contains)]
        [InlineData("<=", FilterType.LessThanOrEqual)]
        [InlineData(">=", FilterType.GreaterThanOrEqual)]
        [InlineData("<", FilterType.LessThan)]
        [InlineData(">", FilterType.GreaterThan)]
        public void TestFilterTheory(string suffix, FilterType type)
        {
            this.TestSingleFilterCase(suffix, type);
        }

        [Fact]
        public void TestMultipleCases()
        {
            var filterRequests = new List<FilterRequest>
            {
                new FilterRequest("p1", FilterType.Equals, "p1"),
                new FilterRequest("p2=", "p2", FilterType.Equals, "p2"),
                new FilterRequest("p3~", "p3", FilterType.Contains, "p3"),
                new FilterRequest("p4<", "p4", FilterType.LessThan, "p4"),
                new FilterRequest("p5>", "p5", FilterType.GreaterThan, "p5"),
                new FilterRequest("p6<=", "p6", FilterType.LessThanOrEqual, "p6"),
                new FilterRequest("p7>=", "p7", FilterType.GreaterThanOrEqual, "p7")
            };
            var filters = filterRequests.ToDictionary(r => r.OriginalProperty, r => r.Filter);
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateFilterContext(filters), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            foreach (var filterRequest in filterRequests)
            {
                Assert.Contains(result, r => r.OriginalProperty == filterRequest.OriginalProperty
                    && r.Property == filterRequest.Property
                    && r.Type == filterRequest.Type
                    && r.Filter == filterRequest.Filter);
            }
        }

        [Fact]
        public void InvalidFilter()
        {
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateFilterContext("{"), this.keys);
            Assert.Throws<FilterInterpreterException>(() => interpreter.ParseRequestQuery());
        }

        // ReSharper disable once UnusedParameter.Local
        private void TestSingleFilterCase(string filterSuffix, FilterType expectedType)
        {
            var filterProperty = $"{Property}{filterSuffix}";
            var interpreter = new FilterByClientRequestInterpreter(
                this.CreateFilterContext(filterProperty, Filter), this.keys);
            var result = interpreter.ParseRequestQuery().ToList();
            Assert.Equal(1, result.Count);
            var request = result.First();
            Assert.Equal(expectedType, request.Type);
            Assert.Equal(Property, request.Property);
            Assert.Equal(filterProperty, request.OriginalProperty);
            Assert.Equal(Filter, request.Filter);
        }

        private IScopedStorage<HttpContext> CreateEmptyFilterContext() =>
            this.CreateFilterContext(new QueryCollection());

        private IScopedStorage<HttpContext> CreateFilterContext(string property, string filter) =>
            this.CreateFilterContext(new Dictionary<string, string> { [property] = filter });

        private IScopedStorage<HttpContext> CreateFilterContext(Dictionary<string, string> filters) =>
            this.CreateFilterContext(JsonConvert.SerializeObject(filters));

        private IScopedStorage<HttpContext> CreateFilterContext(string value)
        {
            return this.CreateFilterContext(new QueryCollection(new Dictionary<string, StringValues>
            {
                [this.keys.Filter] = new StringValues(value)
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
    }
}
