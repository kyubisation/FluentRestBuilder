// <copyright file="EntityUpdatePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Test.ChainPipes.Update
{
    using System.Linq;
    using System.Threading.Tasks;
    using Core.ChainPipes.Update;
    using Microsoft.EntityFrameworkCore;
    using Mocks;
    using Xunit;

    public class EntityUpdatePipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestUpdate()
        {
            const string newName = "TestUpdate";
            this.CreateEntities();
            var scopedContext = this.ResolveScoped<DbContext>();
            var entity = scopedContext.Set<Entity>().First();
            Assert.NotEqual(newName, entity.Name);
            entity.Name = newName;
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                source => new EntityUpdatePipe<Entity>(scopedContext, source));
            await resultPipe.Execute();
            using (var context = this.CreateContext())
            {
                var updatedEntity = context.Entities.Single(e => e.Id == entity.Id);
                Assert.Equal(newName, updatedEntity.Name);
            }
        }
    }
}
