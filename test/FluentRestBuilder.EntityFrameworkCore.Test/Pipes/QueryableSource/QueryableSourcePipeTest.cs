// <copyright file="QueryableSourcePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.QueryableSource
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class QueryableSourcePipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public QueryableSourcePipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterSource()
                .RegisterDbContext<MockDbContext>()
                .RegisterQueryableSourcePipe()
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
        public async Task TestCreationWithoutPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            var parent2 = await this.CreateParentWithChildren();
            var result = await this.controller.FromSource(parent)
                .WithQueryable(f => f.Set<Child>())
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Equal(
                parent.Children.Count + parent2.Children.Count,
                await result.CountAsync());
        }

        [Fact]
        public async Task TestCreationWithPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            await this.CreateParentWithChildren();
            var result = await this.controller.FromSource(parent)
                .MapToQueryable((f, p) => f.Set<Child>().Where(c => c.ParentId == p.Id))
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Equal(parent.Children.Count, await result.CountAsync());
        }

        private async Task<Parent> CreateParentWithChildren()
        {
            using (var context = this.database.Create())
            {
                var id = await context.Parents.CountAsync() + 1;
                var parent = new Parent
                {
                    Id = id,
                    Name = "test",
                    Children = new List<Child>
                    {
                        new Child { Id = 1 + (id * 10), Name = "test1" },
                        new Child { Id = 2 + (id * 10), Name = "test2" }
                    }
                };
                context.Add(parent);
                await context.SaveChangesAsync();
                return parent;
            }
        }
    }
}
