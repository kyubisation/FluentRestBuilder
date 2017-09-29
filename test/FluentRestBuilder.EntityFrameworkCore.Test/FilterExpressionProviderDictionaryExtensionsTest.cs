// <copyright file="FilterExpressionProviderDictionaryExtensionsTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test
{
    using System;
    using System.Globalization;
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.FilterConverters;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class FilterExpressionProviderDictionaryExtensionsTest
    {
        private readonly PersistantDatabase database;
        private readonly IServiceProvider provider;

        public FilterExpressionProviderDictionaryExtensionsTest()
        {
            this.database = new PersistantDatabase();
            this.provider = new ServiceCollection()
                .AddSingleton<IFilterToTypeConverter<int>, FilterToIntegerConverter>()
                .AddSingleton<IFilterToTypeConverter<double>, FilterToDoubleConverter>()
                .AddSingleton<IFilterToTypeConverter<DateTime>, FilterToDateTimeConverter>()
                .AddSingleton<ICultureInfoConversionPriorityCollection, CultureInfoConversionPriorityCollection>()
                .BuildServiceProvider();
        }

        [Theory]
        [InlineData(FilterType.Equals, 1)]
        [InlineData(FilterType.Contains, 11)]
        public void TestStringFilters(FilterType filterType, int expectedResults)
        {
            var entity = this.database.CreateEnumeratedEntities(20).First();
            var dictionary = new FilterExpressionProviderDictionary<Entity>(this.provider)
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
            var dictionary = new FilterExpressionProviderDictionary<Entity>(this.provider)
                .AddIntegerFilters(e => e.Id);
            var filter = dictionary[nameof(Entity.Id)].Resolve(filterType, entity.Id.ToString());
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
        public void TestDoubleFilters(FilterType filterType, int expectedResults)
        {
            var entity = this.database.CreateOtherEntities(20).Skip(10).First();
            var dictionary = new FilterExpressionProviderDictionary<OtherEntity>(this.provider)
                .AddDoubleFilters(e => e.Rate);
            var filter = dictionary[nameof(OtherEntity.Rate)]
                .Resolve(filterType, entity.Rate.ToString(CultureInfo.InvariantCulture));
            using (var context = this.database.Create())
            {
                Assert.Equal(expectedResults, context.OtherEntities.Count(filter));
            }
        }

        [Theory]
        [InlineData(FilterType.Equals, 1)]
        [InlineData(FilterType.GreaterThan, 9)]
        [InlineData(FilterType.GreaterThanOrEqual, 10)]
        [InlineData(FilterType.LessThan, 10)]
        [InlineData(FilterType.LessThanOrEqual, 11)]
        public void TestDateTimeFilters(FilterType filterType, int expectedResults)
        {
            var entity = this.database.CreateOtherEntities(20).Skip(10).First();
            var dictionary = new FilterExpressionProviderDictionary<OtherEntity>(this.provider)
                .AddDateTimeFilters(e => e.CreatedOn);
            var filterString = entity.CreatedOn.ToString(CultureInfo.InvariantCulture);
            var filter = dictionary[nameof(OtherEntity.CreatedOn)].Resolve(filterType, filterString);
            using (var context = this.database.Create())
            {
                Assert.Equal(expectedResults, context.OtherEntities.Count(filter));
            }
        }
    }
}
