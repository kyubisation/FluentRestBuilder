// <copyright file="FilterExpressionProviderDictionaryExtensionsTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.FilterByClientRequest.Expressions
{
    using System.Linq;
    using FluentRestBuilder.Pipes.FilterByClientRequest;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Expressions;
    using Mocks.EntityFramework;
    using Xunit;

    public class FilterExpressionProviderDictionaryExtensionsTest
    {
        private readonly PersistantDatabase database;

        public FilterExpressionProviderDictionaryExtensionsTest()
        {
            this.database = new PersistantDatabase();
        }

        [Theory]
        [InlineData(FilterType.Equals, 1)]
        [InlineData(FilterType.Contains, 11)]
        public void TestStringFilters(FilterType filterType, int expectedResults)
        {
            var entity = this.database.CreateEnumeratedEntities(20).First();
            var dictionary = new FilterExpressionProviderDictionary<Entity>()
                .AddEqualAndContainsStringFilter(e => e.Name);
            var filter = dictionary[nameof(Entity.Name)].Resolve(filterType, entity.Name);
            using (var context = this.database.Create())
            {
                Assert.Equal(expectedResults, context.Entities.Count(filter));
            }
        }

        [Theory]
        [InlineData(FilterType.Equals, 1)]
        [InlineData(FilterType.GreaterThan, 9)]
        [InlineData(FilterType.GreaterThanOrEqual, 10)]
        [InlineData(FilterType.LessThan, 10)]
        [InlineData(FilterType.LessThanOrEqual, 11)]
        public void TestIntegerFilters(FilterType filterType, int expectedResults)
        {
            var entity = this.database.CreateEnumeratedEntities(20).Skip(10).First();
            var dictionary = new FilterExpressionProviderDictionary<Entity>()
                .AddIntegerFilters(e => e.Id);
            var filter = dictionary[nameof(Entity.Id)].Resolve(filterType, entity.Id.ToString());
            using (var context = this.database.Create())
            {
                Assert.Equal(expectedResults, context.Entities.Count(filter));
            }
        }
    }
}
