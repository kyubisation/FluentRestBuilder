// <copyright file="DistributedCacheInputStoragePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.DistributedCacheInputStorage
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Caching.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.Source;
    using Xunit;

    public class DistributedCacheInputStoragePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryDistributedCache cache =
            new MemoryDistributedCache(new MemoryCache(new MemoryCacheOptions()));

        public DistributedCacheInputStoragePipeTest()
        {
            this.provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterDistributedCacheInputStoragePipe()
                .Services
                .AddSingleton<IDistributedCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestStoringEntity()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreInputInDistributedCache(Key)
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            var mapper = this.provider.GetService<IByteMapper<Entity>>();
            var storedEntity = mapper.FromByteArray(await this.cache.GetAsync(Key));
            Assert.Equal(entity, storedEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestStoringEntityWithOptions()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreInputInDistributedCache(Key, new DistributedCacheEntryOptions())
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            var mapper = this.provider.GetService<IByteMapper<Entity>>();
            var storedEntity = mapper.FromByteArray(await this.cache.GetAsync(Key));
            Assert.Equal(entity, storedEntity, new PropertyComparer<Entity>());
        }
    }
}
