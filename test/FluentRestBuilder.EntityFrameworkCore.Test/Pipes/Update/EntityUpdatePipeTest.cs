// <copyright file="EntityUpdatePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Update
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class EntityUpdatePipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public EntityUpdatePipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterStorage()
                .RegisterActionPipe()
                .RegisterSingleOrDefaultPipe()
                .RegisterContext<MockDbContext>()
                .RegisterQueryableSource()
                .RegisterUpdatePipe()
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
        public async Task TestUpdate()
        {
            const string newName = "TestUpdate";
            var entity = this.database.CreateEnumeratedEntities(3).First();
            Assert.NotEqual(newName, entity.Name);

            var result = await this.controller.WithQueryable<Entity>()
                .SingleOrDefault(e => e.Id == entity.Id)
                .Do(e => e.Name = newName)
                .UpdateEntity()
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Id, result.Id);
            using (var context = this.database.Create())
            {
                var updatedEntity = context.Entities.Single(e => e.Id == entity.Id);
                Assert.Equal(newName, updatedEntity.Name);
            }
        }
    }
}
