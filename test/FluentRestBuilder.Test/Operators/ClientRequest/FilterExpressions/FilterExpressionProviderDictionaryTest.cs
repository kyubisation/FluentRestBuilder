// <copyright file="FilterExpressionProviderDictionaryTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterExpressions
{
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class FilterExpressionProviderDictionaryTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public void TestExpressionProvider()
        {
            var entities = this.database.CreateOtherEntities(10);
            var provider = new ServiceCollection()
                .AddFluentRestBuilder()
                .Services
                .BuildServiceProvider();
            var dictionary1 = new FilterExpressionProviderDictionary<OtherEntity>(provider)
                .AddStringFilter(e => e.Name)
                .AddNumericFilter(e => e.Id)
                .AddNumericFilter(e => e.IntValue)
                .AddDateTimeFilter(e => e.CreatedOn)
                .AddBooleanFilter(e => e.Active)
                .AddBooleanFilter(e => e.Status);
            var dictionary2 = new FilterExpressionProviderDictionary<OtherEntity>(provider)
                .AddPropertyFilters();
            foreach (var dictionary in new[] { dictionary1, dictionary2 })
            {
                var emptyExpression = dictionary[nameof(OtherEntity.Name)]
                    .Resolve(FilterType.GreaterThan, "0");
                Assert.Null(emptyExpression);
                var expression = dictionary[nameof(OtherEntity.Name)]
                    .Resolve(FilterType.Contains, "Name");
                using (var context = this.database.Create())
                {
                    var resultEntities = context.OtherEntities
                        .Where(expression)
                        .ToList();
                    Assert.Equal(entities, resultEntities, new PropertyComparer<OtherEntity>());
                }
            }
        }
    }
}
