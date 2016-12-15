// <copyright file="EntityDeletionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Deletion
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Pipes.Deletion;
    using FluentRestBuilder.Test.Common.Mocks;
    using Xunit;

    public class EntityDeletionPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestDeletion()
        {
            this.CreateEntities();
            var entity = Entity.Entities.First();
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                p => new EntityDeletionPipe<Entity>(this.Context, p));
            await resultPipe.Execute();
            using (var context = this.CreateContext())
            {
                Assert.Equal(Entity.Entities.Count - 1, context.Entities.Count());
            }
        }
    }
}
