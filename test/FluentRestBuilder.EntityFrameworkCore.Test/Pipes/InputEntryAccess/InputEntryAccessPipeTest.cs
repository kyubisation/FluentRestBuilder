// <copyright file="InputEntryAccessPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.InputEntryAccess
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class InputEntryAccessPipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public InputEntryAccessPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSingleOrDefaultPipe()
                .RegisterActionPipe()
                .RegisterContext<MockDbContext>()
                .RegisterQueryableSource()
                .RegisterInputEntryAccessPipe()
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
        public async Task TestSimpleAccess()
        {
            var entities = this.database.CreateEnumeratedEntities(3);
            var entity = entities.First();
            var result = await this.controller.WithQueryable<Entity>()
                .SingleOrDefault(e => e.Id == entity.Id)
                .WithEntityEntry(
                    e => Assert.Equal(entity, e.Entity, new PropertyComparer<Entity>()))
                .ToObjectResultOrDefault();
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestLoadReference()
        {
            var parent = this.database.CreateParentsWithChildren(1).First();
            var child = parent.Children.First();
            var result = await this.controller.WithQueryable<Child>()
                .SingleOrDefault(c => c.Id == child.Id)
                .LoadReference(c => c.Parent)
                .ToObjectResultOrDefault();
            Assert.Equal(child, result, new PropertyComparer<Child>());
            Assert.Equal(parent, result.Parent, new PropertyComparer<Parent>());
        }

        [Fact]
        public async Task TestLoadCollection()
        {
            var parent = this.database.CreateParentsWithChildren(1).First();
            var result = await this.controller.WithQueryable<Parent>()
                .SingleOrDefault(p => p.Id == parent.Id)
                .LoadCollection(p => p.Children)
                .ToObjectResultOrDefault();
            Assert.Equal(parent, result, new PropertyComparer<Parent>());
            Assert.True(parent.Children.SequenceEqual(result.Children, new PropertyComparer<Child>()));
        }

        [Fact]
        public async Task TestReload()
        {
            Entity changedEntity = null;
            var entity = this.database.CreateEnumeratedEntities(3).First();
            var result = await this.controller.WithQueryable<Entity>()
                .SingleOrDefault(e => e.Id == entity.Id)
                .Do(e => Assert.Equal(entity, e, new PropertyComparer<Entity>()))
                .Do(e => changedEntity = this.ChangeEntity(e))
                .ReloadEntity()
                .ToObjectResultOrDefault();
            Assert.Equal(changedEntity, result, new PropertyComparer<Entity>());
        }

        private Entity ChangeEntity(Entity source)
        {
            using (var context = this.database.Create())
            {
                var changedEntity = context.Entities.SingleOrDefault(n => n.Id == source.Id);
                changedEntity.Name = nameof(this.TestReload);
                changedEntity.Description = nameof(this.TestReload);
                context.SaveChanges();

                return changedEntity;
            }
        }
    }
}
