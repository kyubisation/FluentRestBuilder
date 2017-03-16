// <copyright file="MemoryCacheInputStoragePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.MemoryCacheInputStorage
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.Source;
    using Xunit;

    public class MemoryCacheInputStoragePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public MemoryCacheInputStoragePipeTest()
        {
            this.provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterMemoryCacheInputStoragePipe()
                .Services
                .AddSingleton<IMemoryCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestStoringEntity()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreInputInMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            var storedEntity = this.cache.Get<Entity>(Key);
            Assert.Equal(entity, storedEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestStoringEntityWithOptions()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreInputInMemoryCache(Key, new MemoryCacheEntryOptions())
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            var storedEntity = this.cache.Get<Entity>(Key);
            Assert.Equal(entity, storedEntity, new PropertyComparer<Entity>());
        }
    }
}
