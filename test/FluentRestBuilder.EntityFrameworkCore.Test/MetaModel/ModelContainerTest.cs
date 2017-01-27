// <copyright file="ModelContainerTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.MetaModel
{
    using System;
    using System.Linq;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Storage;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class ModelContainerTest
    {
        private readonly IServiceProvider provider;

        public ModelContainerTest()
        {
            this.provider = new ServiceCollection()
                .AddScoped<MockDbContext>()
                .AddScoped<IScopedStorage<DbContext>, DbContextScopedStorage<MockDbContext>>()
                .BuildServiceProvider();
        }

        [Fact]
        public void TestUsage()
        {
            var container = new ModelContainer(this.provider);
            var entityType = container.Model.FindEntityType(typeof(Entity));
            Assert.NotNull(entityType);
            var primaryKey = entityType.FindPrimaryKey();
            Assert.NotNull(primaryKey);
            Assert.Equal(1, primaryKey.Properties.Count);
            Assert.Equal(nameof(Entity.Id), primaryKey.Properties.Single().Name);
        }
    }
}
