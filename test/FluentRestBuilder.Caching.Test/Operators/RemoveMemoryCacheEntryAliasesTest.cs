// <copyright file="RemoveMemoryCacheEntryAliasesTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Operators
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class RemoveMemoryCacheEntryAliasesTest
    {
        private readonly IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        private readonly ServiceProvider provider;

        public RemoveMemoryCacheEntryAliasesTest()
        {
            this.provider = new ServiceCollection()
                .AddSingleton(this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestRemovingEntry()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            this.cache.Set(entity.Name, entity);
            await Observable.Single(entity, this.provider)
                .RemoveMemoryCacheEntry(entity.Name);
            var result = this.cache.Get<Entity>(entity.Name);
            Assert.Null(result);
        }

        [Fact]
        public async Task TestRemovingNonExistingEntry()
        {
            var entity = new Entity { Id = 1, Name = "name", Description = "description" };
            var result = this.cache.Get<Entity>(entity.Name);
            Assert.Null(result);
            await Observable.Single(entity, this.provider)
                .RemoveMemoryCacheEntry(entity.Name);
        }
    }
}
