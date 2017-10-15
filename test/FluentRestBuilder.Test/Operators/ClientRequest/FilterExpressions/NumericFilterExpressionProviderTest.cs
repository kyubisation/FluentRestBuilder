// <copyright file="NumericFilterExpressionProviderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterExpressions
{
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.FilterConverters;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class NumericFilterExpressionProviderTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public void TestDefault()
        {
            var entities = this.database.CreateOtherEntities(30)
                .Where(e => e.IntValue == 2)
                .ToList();
            Assert.NotEmpty(entities);
            var provider = new NumericFilterExpressionProvider<OtherEntity, int?>(
                nameof(OtherEntity.IntValue),
                new GenericFilterToTypeConverter<int?>(
                    new CultureInfoConversionPriorityCollection()));
            var expression = provider.Resolve(FilterType.Default, 2.ToString());
            using (var context = this.database.Create())
            {
                var result = context.OtherEntities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<OtherEntity>());
            }
        }
    }
}
