// <copyright file="EntityDeletionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Deletion
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class EntityDeletionPipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public EntityDeletionPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSingleOrDefaultPipe()
                .RegisterDbContext<MockDbContext>()
                .RegisterQueryableSource()
                .RegisterDeletionPipe()
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
        public async Task TestDeletion()
        {
            var entities = this.database.CreateEnumeratedEntities(10);
            var entity = entities.First();
            var result = await this.controller.WithQueryable<Entity>()
                .SingleOrDefault(e => e.Id == entity.Id)
                .DeleteEntity()
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Id, result.Id);
            using (var context = this.database.Create())
            {
                Assert.Null(context.Entities.SingleOrDefault(e => e.Id == entity.Id));
            }
        }
    }
}
