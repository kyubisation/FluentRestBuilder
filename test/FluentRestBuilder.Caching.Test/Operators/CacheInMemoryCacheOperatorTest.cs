// <copyright file="CacheInMemoryCacheOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class CacheInMemoryCacheOperatorTest
    {
        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        private readonly ServiceProvider provider;

        public CacheInMemoryCacheOperatorTest()
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
                .CacheInMemoryCache(entity.Name);
            var exists = this.cache.TryGetValue(entity.Name, out var resultObject);
            Assert.True(exists);
            Assert.Equal(entity, resultObject as Entity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestAccessingCache()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            this.cache.Set(entity.Name, entity);
            var lazyEntity = new Lazy<Entity>(() => entity);
            var resultEntity = await Observable.AsyncSingle(lazyEntity, this.provider)
                .CacheInMemoryCache(entity.Name);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
            Assert.False(lazyEntity.IsValueCreated);
        }
    }
}
