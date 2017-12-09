// <copyright file="FilterExpressionBuilderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class FilterExpressionBuilderTest
    {
        private const int IntComparisonValue = 4;

        private readonly PersistantDatabase database = new PersistantDatabase();

        public static IEnumerable<object[]> IntComparing()
        {
            return new Dictionary<
                    Func<Entity, bool>,
                    Func<FilterExpressionBuilder<Entity, int>, Expression<Func<Entity, bool>>>>
                {
                    [e => e.Id > IntComparisonValue] = b => b.CreateGreaterThanExpression(),
                    [e => e.Id >= IntComparisonValue] = b => b.CreateGreaterThanOrEqualExpression(),
                    [e => e.Id < IntComparisonValue] = b => b.CreateLessThanExpression(),
                    [e => e.Id <= IntComparisonValue] = b => b.CreateLessThanOrEqualExpression(),
            }
                .Select(p => new object[] { p.Key, p.Value });
        }

        [Fact]
        public void TestEquals()
        {
            var entity = this.database.CreateOtherEntities(3)
                .First();
            var expressions = new[]
            {
                new FilterExpressionBuilder<OtherEntity, string>(
                        nameof(OtherEntity.Name), entity.Name)
                    .CreateEqualsExpression(),
                new FilterExpressionBuilder<OtherEntity, int>(
                        nameof(OtherEntity.Id), entity.Id)
                    .CreateEqualsExpression(),
                new FilterExpressionBuilder<OtherEntity, int?>(
                        nameof(OtherEntity.IntValue), entity.IntValue)
                    .CreateEqualsExpression(),
                new FilterExpressionBuilder<OtherEntity, double>(
                        nameof(OtherEntity.Rate), entity.Rate)
                    .CreateEqualsExpression(),
                new FilterExpressionBuilder<OtherEntity, bool?>(
                        nameof(OtherEntity.Status), entity.Status)
                    .CreateEqualsExpression(),
                new FilterExpressionBuilder<OtherEntity, DateTime>(
                        nameof(OtherEntity.CreatedOn), entity.CreatedOn)
                    .CreateEqualsExpression(),
            };
            foreach (var expression in expressions)
            {
                using (var context = this.database.Create())
                {
                    var result = context.OtherEntities
                        .Where(expression)
                        .Single();
                    Assert.Equal(entity, result, new PropertyComparer<OtherEntity>());
                }
            }
        }

        [Fact]
        public void TestNotEqaul()
        {
            var entities1 = this.database.CreateSimilarEntities(3, "Name 1");
            var entities2 = this.database.CreateSimilarEntities(3, "Name 2");
            var entities = entities1.Concat(entities2).ToList();
            this.database.CreateSimilarEntities(3, "Name 3");
            var builder = new FilterExpressionBuilder<Entity, string>(nameof(Entity.Name), "Name 3");
            var expression = builder.CreateNotEqualExpression();
            using (var context = this.database.Create())
            {
                var result = context.Entities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }

        [Fact]
        public void TestContains()
        {
            var entities = this.database.CreateSimilarEntities(3, "Name 1");
            this.database.CreateSimilarEntities(3, "Name 2");
            this.database.CreateSimilarEntities(3, "Name 3");
            var builder = new FilterExpressionBuilder<Entity, string>(nameof(Entity.Name), "e 1");
            var expression = builder.CreateContainsExpression();
            using (var context = this.database.Create())
            {
                var result = context.Entities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }

        [Fact]
        public void TestStartsWith()
        {
            var entities = this.database.CreateSimilarEntities(3, "Name 1");
            this.database.CreateSimilarEntities(3, "Test 2");
            this.database.CreateSimilarEntities(3, "Asdf 3");
            var builder = new FilterExpressionBuilder<Entity, string>(nameof(Entity.Name), "Name");
            var expression = builder.CreateStartsWithExpression();
            using (var context = this.database.Create())
            {
                var result = context.Entities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }

        [Fact]
        public void TestEndsWith()
        {
            var entities = this.database.CreateSimilarEntities(3, "Name 1");
            this.database.CreateSimilarEntities(3, "Test 2");
            this.database.CreateSimilarEntities(3, "Asdf 3");
            var builder = new FilterExpressionBuilder<Entity, string>(nameof(Entity.Name), " 1");
            var expression = builder.CreateEndsWithExpression();
            using (var context = this.database.Create())
            {
                var result = context.Entities
                    .Where(expression)
                    .ToList();
                Assert.Equal(entities, result, new PropertyComparer<Entity>());
            }
        }

        [Theory]
        [MemberData(nameof(IntComparing))]
        public void TestIntComparison(
            Func<Entity, bool> filter,
            Func<FilterExpressionBuilder<Entity, int>, Expression<Func<Entity, bool>>> expressionFactory)
        {
            var entities = this.database.CreateEnumeratedEntities(10)
                .Where(filter)
                .ToList();
            var builder = new FilterExpressionBuilder<Entity, int>(
                nameof(Entity.Id), IntComparisonValue);
            var expression = expressionFactory(builder);
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
