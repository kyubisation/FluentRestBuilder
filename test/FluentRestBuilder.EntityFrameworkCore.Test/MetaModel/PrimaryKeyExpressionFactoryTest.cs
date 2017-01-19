// <copyright file="PrimaryKeyExpressionFactoryTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EntityFrameworkCore.MetaModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Storage;
    using Xunit;

    public class PrimaryKeyExpressionFactoryTest
    {
        private readonly PersistantDatabase database;
        private readonly IServiceProvider provider;

        public PrimaryKeyExpressionFactoryTest()
        {
            this.database = new PersistantDatabase();
            this.provider = new ServiceCollection()
                .AddScoped(p => this.database.Create())
                .AddScoped<IScopedStorage<DbContext>, DbContextScopedStorage<MockDbContext>>()
                .AddSingleton<IModelContainer, ModelContainer>()
                .AddSingleton(typeof(IPredicateBuilder<>), typeof(PredicateBuilder<>))
                .AddSingleton(
                    typeof(IPrimaryKeyExpressionFactory<>), typeof(PrimaryKeyExpressionFactory<>))
                .BuildServiceProvider();
        }

        [Fact]
        public void TestExpressionCreation()
        {
            var factory = this.provider.GetRequiredService<IPrimaryKeyExpressionFactory<Entity>>();
            var entity = this.database.CreateEnumeratedEntities(3).First();
            var expression = factory.CreatePrimaryKeyFilterExpression(new object[] { entity.Id });
            using (var context = this.database.Create())
            {
                var result = context.Entities.Single(expression);
                Assert.Equal(entity, result, new PropertyComparer<Entity>());
            }
        }

        [Fact]
        public void TestExpressionCreationWithMultipleKeys()
        {
            var factory = this.provider
                .GetRequiredService<IPrimaryKeyExpressionFactory<MultiKeyEntity>>();
            var entity = this.database.CreateMultiKeyEntities(3).First();
            var expression = factory.CreatePrimaryKeyFilterExpression(
                new object[] { entity.FirstId, entity.SecondId });
            using (var context = this.database.Create())
            {
                var result = context.MultiKeyEntities.Single(expression);
                Assert.Equal(entity, result, new PropertyComparer<MultiKeyEntity>());
            }
        }

        [Fact]
        public void TestKeyMismatch()
        {
            var factory = this.provider.GetRequiredService<IPrimaryKeyExpressionFactory<Entity>>();
            Assert.Throws<PrimaryKeyMismatchException>(
                () => factory.CreatePrimaryKeyFilterExpression(new object[] { 1, 2 }));
        }

        [Theory]
        [ClassData(typeof(MultipleKeyList))]
        public void TestKeyMismatchForMultipleKeys(object[] keys)
        {
            var factory = this.provider.GetRequiredService<IPrimaryKeyExpressionFactory<MultiKeyEntity>>();
            Assert.Throws<PrimaryKeyMismatchException>(
                () => factory.CreatePrimaryKeyFilterExpression(keys));
        }

        private class MultipleKeyList : List<object[]>
        {
            public MultipleKeyList()
            {
                this.AddKeys(1)
                    .AddKeys(string.Empty)
                    .AddKeys(1, "2")
                    .AddKeys(1, 1, 1)
                    .AddKeys();
            }

            private MultipleKeyList AddKeys(params object[] keys)
            {
                this.Add(new object[] { keys });
                return this;
            }
        }
    }
}
