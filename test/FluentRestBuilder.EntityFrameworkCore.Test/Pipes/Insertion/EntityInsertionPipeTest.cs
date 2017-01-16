// <copyright file="EntityInsertionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Insertion
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using FluentRestBuilder.Test.Common.Mocks;
    using FluentRestBuilder.Test.Common.Mocks.EntityFramework;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class EntityInsertionPipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public EntityInsertionPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterStorage()
                .RegisterSource()
                .RegisterContext<MockDbContext>()
                .RegisterInsertionPipe()
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
        public async Task TestInsertion()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var result = await this.controller.FromSource(entity)
                .InsertEntity()
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            using (var context = this.database.Create())
            {
                Assert.Equal(1, context.Entities.Count(e => e.Id == entity.Id));
            }
        }
    }
}
