// <copyright file="BooleanFilterExpressionProviderTest.cs" company="Kyubisation">
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

    public class BooleanFilterExpressionProviderTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public void TestDefault()
        {
            var entities = this.database.CreateOtherEntities(30)
                .Where(e => e.Active)
                .ToList();
            var provider = new BooleanFilterExpressionProvider<OtherEntity, bool>(
                nameof(OtherEntity.Active), new FilterToBooleanConverter());
            var expression = provider.Resolve(FilterType.Default, true.ToString());
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
