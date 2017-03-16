// <copyright file="MemoryCacheInputBridgePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.MemoryCacheInputBridge
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.LazySource;
    using Xunit;

    public class MemoryCacheInputBridgePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public MemoryCacheInputBridgePipeTest()
        {
            this.provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterMemoryCacheInputBridgePipe()
                .Services
                .AddSingleton<IMemoryCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestNoCacheEntryAvailable()
        {
            var lazyEntity = new Lazy<Entity>(() => new Entity { Id = 1 });
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.True(lazyEntity.IsValueCreated);
            Assert.Same(lazyEntity.Value, result);
        }

        [Fact]
        public async Task TestCacheEntryAvailable()
        {
            var entity = new Entity { Id = 1 };
            this.cache.Set(Key, entity);

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.False(lazyEntity.IsValueCreated);
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestTypeMismatch()
        {
            var entity = new Entity { Id = 1 };
            this.cache.Set(Key, new MultiKeyEntity());

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.True(lazyEntity.IsValueCreated);
            Assert.Same(lazyEntity.Value, result);
        }
    }
}
