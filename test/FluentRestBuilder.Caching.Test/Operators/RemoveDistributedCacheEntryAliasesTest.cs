// <copyright file="RemoveDistributedCacheEntryAliasesTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System.Threading.Tasks;
    using Caching.Operators.DistributedCache;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Mocks.EntityFramework;
    using Xunit;

    public class RemoveDistributedCacheEntryAliasesTest
    {
        private readonly IDistributedCache cache = new MemoryDistributedCache(
            new OptionsWrapper<MemoryDistributedCacheOptions>(
                new MemoryDistributedCacheOptions()));

        private readonly ServiceProvider provider;

        public RemoveDistributedCacheEntryAliasesTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton(this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestRemovingEntry()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            var jsonMapper = new JsonMapper<Entity>();
            await this.cache.SetAsync(entity.Name, jsonMapper.ToByteArray(entity));
            await Observable.Single(entity, this.provider)
                .RemoveDistributedCacheEntry(entity.Name);
            var result = await this.cache.GetAsync(entity.Name);
            Assert.Null(result);
        }

        [Fact]
        public async Task TestRemovingNonExistingEntry()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            var result = await this.cache.GetAsync(entity.Name);
            Assert.Null(result);
            await Observable.Single(entity, this.provider)
                .RemoveDistributedCacheEntry(entity.Name);
        }
    }
}
