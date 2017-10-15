// <copyright file="IntegrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Observables
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class IntegrationTest
    {
        private readonly PersistantDatabase database = new PersistantDatabase();

        private readonly MockController controller;

        public IntegrationTest()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IScopedStorage<DbContext>>(
                    s => new ScopedStorage<DbContext> { Value = this.database.Create() })
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        [Fact]
        public async Task TestCreateEntitySingle()
        {
            var entity = this.database.CreateEnumeratedEntities(10).First();
            var resultEntity = await this.controller
                .CreateEntitySingle<Entity>(e => e.Id == entity.Id);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestCreateEntitySingleViaFind()
        {
            var entity = this.database.CreateEnumeratedEntities(10).First();
            var resultEntity = await this.controller.CreateEntitySingle<Entity>(entity.Id);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestCreateQueryableSingle()
        {
            var entity = this.database.CreateEnumeratedEntities(10).First();
            var resultEntity = await this.controller
                .CreateQueryableSingle<Entity>()
                .SingleAsync(e => e.Id == entity.Id);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }
    }
}
