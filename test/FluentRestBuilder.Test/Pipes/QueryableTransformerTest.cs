// <copyright file="QueryableTransformerTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Pipes;
    using Xunit;

    public class QueryableTransformerTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly IQueryableTransformer<Entity> transformer;
        private MockDbContext context;

        public QueryableTransformerTest()
        {
            this.transformer = new QueryableTransformer<Entity>();
            this.database = new PersistantDatabase();
            this.context = this.database.Create();
        }

        public void Dispose()
        {
            if (this.context == null)
            {
                return;
            }

            this.context.Dispose();
            this.context = null;
        }

        [Fact]
        public async Task TestToList()
        {
            this.database.CreateEnumeratedEntities(3);
            var entities = await this.transformer.ToList(this.context.Entities);
            Assert.Equal(3, entities.Count);
        }

        [Fact]
        public async Task TestSingleOrDefault()
        {
            var firstEntity = this.database
                .CreateEnumeratedEntities(3)
                .First();
            var queryable = this.context.Entities.Where(e => e.Id == firstEntity.Id);
            var entity = await this.transformer.SingleOrDefault(queryable);
            Assert.Equal(firstEntity.Id, entity.Id);
        }

        [Fact]
        public async Task TestDuplicateSingleOrDefault()
        {
            var firstEntity = this.database
                .CreateSimilarEntities(3)
                .First();
            var queryable = this.context.Entities.Where(e => e.Name == firstEntity.Name);
            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await this.transformer.SingleOrDefault(queryable));
        }

        [Fact]
        public async Task TestMissingSingleOrDefault()
        {
            var queryable = this.context.Entities.Where(e => e.Id == 1);
            var entity = await this.transformer.SingleOrDefault(queryable);
            Assert.Null(entity);
        }

        [Fact]
        public async Task TestFirstOrDefault()
        {
            var firstEntity = this.database
                .CreateSimilarEntities(3)
                .First();
            var queryable = this.context.Entities.Where(e => e.Name == firstEntity.Name);
            var entity = await this.transformer.FirstOrDefault(queryable);
            Assert.Equal(firstEntity.Name, entity.Name);
        }

        [Fact]
        public async Task TestMissingFirstOrDefault()
        {
            var queryable = this.context.Entities.Where(e => e.Id == 1);
            var entity = await this.transformer.FirstOrDefault(queryable);
            Assert.Null(entity);
        }

        [Fact]
        public async Task TestEmptyCount()
        {
            var count = await this.transformer.Count(this.context.Entities);
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task TestCount()
        {
            const int amount = 3;
            this.database.CreateEnumeratedEntities(amount);
            var count = await this.transformer.Count(this.context.Entities);
            Assert.Equal(amount, count);
        }
    }
}
