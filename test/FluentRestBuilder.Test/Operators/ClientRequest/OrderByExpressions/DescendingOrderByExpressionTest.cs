// <copyright file="DescendingOrderByExpressionTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.OrderByExpressions
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Operators.ClientRequest.OrderByExpressions;
    using Microsoft.EntityFrameworkCore;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class DescendingOrderByExpressionTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        [Fact]
        public async Task TestSimpleOrderBy()
        {
            var entities = this.database.CreateEnumeratedEntities(5)
                .OrderByDescending(e => e.Id)
                .ToList();
            var orderByExpression = new DescendingOrderByExpression<Entity, int>(e => e.Id);
            using (var context = this.database.Create())
            {
                var result = await orderByExpression
                    .OrderBy(context.Entities)
                    .ToListAsync();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }

        [Fact]
        public async Task TestThenOrderBy()
        {
            var entities = this.database.CreateSimilarEntities(3, "Name2")
                .Concat(this.database.CreateSimilarEntities(3, "Name1"))
                .OrderBy(e => e.Name)
                .ThenByDescending(e => e.Id)
                .ToList();
            var orderByExpression = new DescendingOrderByExpression<Entity, int>(e => e.Id);
            using (var context = this.database.Create())
            {
                var result = await orderByExpression
                    .ThenBy(context.Entities.OrderBy(e => e.Name))
                    .ToListAsync();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }
    }
}
