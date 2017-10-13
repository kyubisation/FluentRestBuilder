// <copyright file="OrderByExpressionDictionaryTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.OrderByExpressions
{
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest.Interpreters.Requests;
    using FluentRestBuilder.Operators.ClientRequest.OrderByExpressions;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class OrderByExpressionDictionaryTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public void TestOrderByConfiguration()
        {
            var entities = this.database.CreateEnumeratedEntities(10)
                .OrderByDescending(e => e.Name)
                .ToList();
            var dictionary1 = new OrderByExpressionDictionary<Entity>()
                .Add(e => e.Name);
            var dictionary2 = new OrderByExpressionDictionary<Entity>()
                .AddProperties();
            foreach (var dictionary in new[] { dictionary1, dictionary2 })
            {
                using (var context = this.database.Create())
                {
                    var expression = dictionary[nameof(Entity.Name)]
                        .Create(OrderByDirection.Descending);
                    var result = expression
                        .OrderBy(context.Entities)
                        .ToList();
                    Assert.Equal(entities, result, new PropertyComparer<Entity>());
                }
            }
        }
    }
}
