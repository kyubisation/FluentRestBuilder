// <copyright file="QueryablePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Queryable
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class QueryablePipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public QueryablePipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterQueryablePipe()
                .RegisterSingleOrDefaultPipe()
                .RegisterDbContext<MockDbContext>()
                .RegisterQueryableSource()
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
        public async Task TestAsNoTracking()
        {
            var entity = this.database.CreateEnumeratedEntities(3).First();
            var result = await this.controller.WithQueryable<Entity>()
                .AsNoTracking()
                .SingleOrDefault(entity.Id)
                .ToObjectResultOrDefault();
            var context = this.controller.HttpContext.RequestServices
                .GetService<MockDbContext>();
            Assert.Equal(EntityState.Detached, context.Entry(result).State);
        }

        [Fact]
        public async Task TestInclude()
        {
            var parent = this.database.CreateParentsWithChildren(3).First();
            var result = await this.controller.WithQueryable<Parent>()
                .Include(p => p.Children)
                .SingleOrDefault(p => p.Id == parent.Id)
                .ToObjectResultOrDefault();
            Assert.Equal(parent, result, new PropertyComparer<Parent>());
            Assert.True(parent.Children.SequenceEqual(result.Children, new PropertyComparer<Child>()));
        }
    }
}
