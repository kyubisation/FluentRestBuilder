// <copyright file="EntityCollectionSourcePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.ChainPipes.EntityCollectionSource
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Core.ChainPipes.EntityCollectionSource;
    using Microsoft.EntityFrameworkCore;
    using Mocks;
    using Xunit;

    public class EntityCollectionSourcePipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestCreationWithoutPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            var parent2 = await this.CreateParentWithChildren();
            var result = MockSourcePipe<Parent>.CreateCompleteChain(
                parent,
                this.ServiceProvider,
                p => new EntityCollectionSourcePipe<Parent, Child>(
                    null, this.ResolveScoped<IQueryableFactory<Child>>().Queryable, p));
            await result.Execute();
            Assert.NotNull(result.Input);
            Assert.Equal(
                parent.Children.Count + parent2.Children.Count,
                await result.Input.CountAsync());
        }

        [Fact]
        public async Task TestCreationWithPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            await this.CreateParentWithChildren();
            var result = MockSourcePipe<Parent>.CreateCompleteChain(
                parent,
                this.ServiceProvider,
                pipe => new EntityCollectionSourcePipe<Parent, Child>(
                    (q, p) => q.Where(c => c.ParentId == p.Id),
                    this.ResolveScoped<IQueryableFactory<Child>>().Queryable,
                    pipe));
            await result.Execute();
            Assert.NotNull(result.Input);
            Assert.Equal(parent.Children.Count, await result.Input.CountAsync());
        }

        private async Task<Parent> CreateParentWithChildren()
        {
            using (var context = this.CreateContext())
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
