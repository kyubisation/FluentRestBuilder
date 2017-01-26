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
    }
}
