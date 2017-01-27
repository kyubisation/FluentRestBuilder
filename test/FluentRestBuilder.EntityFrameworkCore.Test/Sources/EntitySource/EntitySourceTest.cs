// <copyright file="EntitySourceTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Sources.EntitySource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class EntitySourceTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public EntitySourceTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterContext<MockDbContext>()
                .RegisterEntitySource()
                .Services
                .AddScoped(p => this.database.Create())
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        public void Dispose()
        {
            this.controller.Dispose();
        }

        [Fact]
        public async Task TestMissingEntity()
        {
            var result = await this.controller.WithEntity<Entity>(1)
                .ToObjectResultOrDefault();
            Assert.Null(result);
        }

        [Fact]
        public async Task TestExistingEntity()
        {
            var entity = this.database.CreateEnumeratedEntities(1).First();
            var result = await this.controller.WithEntity<Entity>(entity.Id)
                .ToObjectResultOrDefault();
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }
    }
}
