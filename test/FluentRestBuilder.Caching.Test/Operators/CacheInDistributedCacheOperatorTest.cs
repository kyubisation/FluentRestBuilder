// <copyright file="CacheInDistributedCacheOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        private const string Key = nameof(CacheInDistributedCacheOperatorTest);

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

        public static IEnumerable<object[]> CacheInDistributedCacheFunctions() =>
            new List<Func<IProviderObservable<Entity>, IProviderObservable<Entity>>>
                {
                    o => o.CacheInDistributedCache(Key),
                    o => o.CacheInDistributedCache(Key, DateTimeOffset.Now.AddHours(1)),
                    o => o.CacheInDistributedCache(Key, TimeSpan.FromHours(1)),
                    o => o.CacheInDistributedCache(Key, e => new DistributedCacheEntryOptions()),
                }
                .Select(f => new object[] { f });

        [Theory]
        [MemberData(nameof(CacheInDistributedCacheFunctions))]
        public async Task TestCreatingEntry(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            await function(Observable.Single(entity, this.provider));
            var bytes = await this.cache.GetAsync(Key);
            var resultEntity = new JsonMapper<Entity>().FromByteArray(bytes);
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
        }

        [Theory]
        [MemberData(nameof(CacheInDistributedCacheFunctions))]
        public async Task TestAccessingCache(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            var jsonMapper = new JsonMapper<Entity>();
            await this.cache.SetAsync(Key, jsonMapper.ToByteArray(entity));
            var lazyEntity = new Lazy<Entity>(() => entity);
            var resultEntity = await function(Observable.AsyncSingle(lazyEntity, this.provider));
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
            Assert.False(lazyEntity.IsValueCreated);
        }
    }
}
