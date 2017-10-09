// <copyright file="CacheInMemoryCacheOperatorTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Primitives;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class CacheInMemoryCacheOperatorTest
    {
        private const string Key = nameof(CacheInDistributedCacheOperatorTest);

        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        private readonly ServiceProvider provider;

        public CacheInMemoryCacheOperatorTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton(this.cache)
                .BuildServiceProvider();
        }

        public static IEnumerable<object[]> CacheInMemoryCacheFunctions() =>
            new List<Func<IProviderObservable<Entity>, IProviderObservable<Entity>>>
                {
                    o => o.CacheInMemoryCache(Key),
                    o => o.CacheInMemoryCache(Key, DateTimeOffset.Now.AddHours(1)),
                    o => o.CacheInMemoryCache(Key, TimeSpan.FromHours(1)),
                    o => o.CacheInMemoryCache(
                        Key, new CancellationChangeToken(new CancellationToken(false))),
                }
                .Select(f => new object[] { f });

        [Theory]
        [MemberData(nameof(CacheInMemoryCacheFunctions))]
        public async Task TestCreatingEntry(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            await function(Observable.Single(entity, this.provider));
            var exists = this.cache.TryGetValue(Key, out var resultObject);
            Assert.True(exists);
            Assert.Equal(entity, resultObject as Entity, new PropertyComparer<Entity>());
        }

        [Theory]
        [MemberData(nameof(CacheInMemoryCacheFunctions))]
        public async Task TestAccessingCache(
            Func<IProviderObservable<Entity>, IProviderObservable<Entity>> function)
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            this.cache.Set(Key, entity);
            var lazyEntity = new Lazy<Entity>(() => entity);
            var resultEntity = await function(Observable.AsyncSingle(lazyEntity, this.provider));
            Assert.Equal(entity, resultEntity, new PropertyComparer<Entity>());
            Assert.False(lazyEntity.IsValueCreated);
        }
    }
}
