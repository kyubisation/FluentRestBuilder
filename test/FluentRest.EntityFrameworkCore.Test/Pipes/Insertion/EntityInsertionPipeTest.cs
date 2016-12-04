// <copyright file="EntityInsertionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.Pipes.Insertion
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core.Pipes.Insertion;
    using EntityFrameworkCore.Pipes.Insertion;
    using EntityFrameworkCore.Test;
    using EntityFrameworkCore.Test.Mocks;
    using Xunit;

    public class EntityInsertionPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestInsertion()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity, this.ServiceProvider, p => new EntityInsertionPipe<Entity>(this.Context, p));
            await resultPipe.Execute();
            using (var context = this.CreateContext())
            {
                Assert.Equal(1, context.Entities.Count(e => e.Id == entity.Id));
            }
        }
    }
}
