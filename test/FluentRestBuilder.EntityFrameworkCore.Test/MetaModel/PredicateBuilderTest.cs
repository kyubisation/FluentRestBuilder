// <copyright file="PredicateBuilderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.MetaModel
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using EntityFrameworkCore.MetaModel;
    using Mocks.EntityFramework;
    using Xunit;

    public class PredicateBuilderTest
    {
        private readonly PersistantDatabase database;
        private readonly PredicateBuilder<Entity> predicateBuilder;

        public PredicateBuilderTest()
        {
            this.database = new PersistantDatabase();
            this.predicateBuilder = new PredicateBuilder<Entity>();
        }

        [Fact]
        public void TestJoiningByAnd()
        {
            var entity = this.database.CreateEnumeratedEntities(3).First();
            Expression<Func<Entity, bool>> predicate1 = e => e.Name == entity.Name;
            Expression<Func<Entity, bool>> predicate2 = e => e.Description == entity.Description;
            var predicate = this.predicateBuilder.JoinExpressionsByAnd(predicate1, predicate2);
            using (var context = this.database.Create())
            {
                var result = context.Entities.Single(predicate);
                Assert.Equal(entity.Id, result.Id);
            }
        }

        [Fact]
        public void TestJoiningByOr()
        {
            var entities = this.database.CreateEnumeratedEntities(3);
            var firstEntity = entities.First();
            var lastEntity = entities.Last();
            Expression<Func<Entity, bool>> predicate1 = e => e.Id == firstEntity.Id;
            Expression<Func<Entity, bool>> predicate2 = e => e.Id == lastEntity.Id;
            var predicate = this.predicateBuilder.JoinExpressionsByOr(predicate1, predicate2);
            using (var context = this.database.Create())
            {
                var result = context.Entities.Where(predicate).ToList();
                Assert.Equal(2, result.Count);
                Assert.Equal(firstEntity.Id, result.First().Id);
                Assert.Equal(lastEntity.Id, result.Last().Id);
            }
        }
    }
}
