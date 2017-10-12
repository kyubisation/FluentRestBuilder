// <copyright file="StringFilterExpressionProviderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterExpressions
{
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class StringFilterExpressionProviderTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public void TestDefault()
        {
            var entities = this.database.CreateSimilarEntities(3, "Name 1");
            this.database.CreateSimilarEntities(3, "Name 2");
            this.database.CreateSimilarEntities(3, "Name 3");
            Assert.NotEmpty(entities);
            var provider = new StringFilterExpressionProvider<Entity>(nameof(Entity.Name));
            var expression = provider.Resolve(FilterType.Default, entities.First().Name);
            using (var context = this.database.Create())
            {
                var result = context.Entities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }
    }
}
