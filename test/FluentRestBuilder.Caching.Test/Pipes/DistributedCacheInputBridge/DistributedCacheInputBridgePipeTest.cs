// <copyright file="DistributedCacheInputBridgePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.DistributedCacheInputBridge
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using Caching.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.LazySource;
    using Xunit;

    public class DistributedCacheInputBridgePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryDistributedCache cache =
            new MemoryDistributedCache(new MemoryCache(new MemoryCacheOptions()));

        public DistributedCacheInputBridgePipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterDistributedCacheInputBridgePipe()
                .Services
                .AddSingleton<IDistributedCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestNoCacheEntryAvailable()
        {
            var lazyEntity = new Lazy<Entity>(() => new Entity { Id = 1 });
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInDistributedCache(Key)
                .ToObjectResultOrDefault();
            Assert.True(lazyEntity.IsValueCreated);
            Assert.Same(lazyEntity.Value, result);
        }

        [Fact]
        public async Task TestCacheEntryAvailable()
        {
            var entity = new Entity { Id = 1 };
            var mapper = this.provider.GetService<IByteMapper<Entity>>();
            await this.cache.SetAsync(Key, mapper.ToByteArray(entity));

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInDistributedCache(Key)
                .ToObjectResultOrDefault();
            Assert.False(lazyEntity.IsValueCreated);
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestFaultyCacheEntryAvailable()
        {
            var entity = new Entity { Id = 1 };
            var mapper = this.provider.GetService<IByteMapper<Entity>>();
            var bytes = mapper.ToByteArray(entity);
            await this.cache.SetAsync(Key, bytes.Take(bytes.Length / 2).ToArray());

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfInputAvailableInDistributedCache(Key)
                .ToObjectResultOrDefault();
            Assert.True(lazyEntity.IsValueCreated);
            Assert.Same(lazyEntity.Value, result);
        }
    }
}
