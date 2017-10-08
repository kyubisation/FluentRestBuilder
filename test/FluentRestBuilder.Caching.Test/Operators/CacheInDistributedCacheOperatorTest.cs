// <copyright file="CacheInDistributedCacheOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Caching.Operators.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class CacheInDistributedCacheOperatorTest
    {
        private readonly IDistributedCache cache = new MemoryDistributedCache(
            new OptionsWrapper<MemoryDistributedCacheOptions>(
                new MemoryDistributedCacheOptions()));

        private readonly ServiceProvider provider;

        public CacheInDistributedCacheOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton(this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestCreatingEntry()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            await Observable.Single(entity, this.provider)
                .CacheInDistributedCache(entity.Name);
            var bytes = await this.cache.GetAsync(entity.Name);
            var resultEntity = new JsonMapper<Entity>().FromByteArray(bytes);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestAccessingCache()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            var jsonMapper = new JsonMapper<Entity>();
            await this.cache.SetAsync(entity.Name, jsonMapper.ToByteArray(entity));
            var lazyEntity = new Lazy<Entity>(() => entity);
            var resultEntity = await Observable.AsyncSingle(lazyEntity, this.provider)
                .CacheInDistributedCache(entity.Name);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
            Assert.False(lazyEntity.IsValueCreated);
        }
    }
}
