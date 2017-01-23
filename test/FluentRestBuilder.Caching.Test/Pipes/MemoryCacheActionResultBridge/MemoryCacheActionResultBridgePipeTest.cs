// <copyright file="MemoryCacheActionResultBridgePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.MemoryCacheActionResultBridge
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.LazySource;
    using Xunit;

    public class MemoryCacheActionResultBridgePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public MemoryCacheActionResultBridgePipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterMemoryCacheActionResultBridgePipe()
                .Services
                .AddSingleton<IMemoryCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestNoCacheEntryAvailable()
        {
            var lazyEntity = new Lazy<Entity>(() => new Entity { Id = 1 });
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfActionResultAvailableInMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.True(lazyEntity.IsValueCreated);
            Assert.Same(lazyEntity.Value, result);
        }

        [Fact]
        public async Task TestCacheEntryAvailable()
        {
            var entity = new Entity { Id = 1 };
            var actionResult = new MockActionResult(entity);
            this.cache.Set(Key, actionResult);

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfActionResultAvailableInMemoryCache(Key)
                .ToMockResultPipe()
                .Execute();
            Assert.False(lazyEntity.IsValueCreated);
            Assert.Same(actionResult, result);
            var objectResult = (ObjectResult)result;
            Assert.Equal(entity, objectResult.Value as Entity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestTypeMismatch()
        {
            var entity = new Entity { Id = 1 };
            this.cache.Set(Key, entity);

            var lazyEntity = new Lazy<Entity>(() => entity);
            var result = await new LazySource<Entity>(() => lazyEntity.Value, this.provider)
                .BridgeIfActionResultAvailableInMemoryCache(Key)
                .ToMockResultPipe()
                .Execute();
            Assert.True(lazyEntity.IsValueCreated);
            var objectResult = (ObjectResult)result;
            Assert.Same(lazyEntity.Value, objectResult.Value);
        }
    }
}
