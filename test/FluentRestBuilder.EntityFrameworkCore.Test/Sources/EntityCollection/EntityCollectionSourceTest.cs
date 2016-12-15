// <copyright file="EntityCollectionSourceTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Sources.EntityCollection
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using EntityFrameworkCore.Sources.EntityCollection;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Test.Common.Mocks;
    using Storage;
    using Xunit;

    public class EntityCollectionSourceTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestBareUseCase()
        {
            this.CreateEntities();
            var source = this.CreateSource();
            var resultPipe = new MockResultPipe<IQueryable<Entity>>(source);
            await resultPipe.Execute();
            Assert.Equal(Entity.Entities.Count, resultPipe.Input.Count());
        }

        private EntityCollectionSource<Entity> CreateSource()
        {
            return new EntityCollectionSource<Entity>(
                this.ResolveScoped<IQueryableFactory<Entity>>().Queryable,
                new ScopedStorage<PaginationMetaInfo>(),
                this.ServiceProvider);
        }
    }
}
