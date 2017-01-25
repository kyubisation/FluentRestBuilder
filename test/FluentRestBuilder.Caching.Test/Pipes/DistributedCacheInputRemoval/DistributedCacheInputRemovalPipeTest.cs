// <copyright file="DistributedCacheInputRemovalPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.DistributedCacheInputRemoval
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.Source;
    using Xunit;

    public class DistributedCacheInputRemovalPipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryDistributedCache cache =
            new MemoryDistributedCache(new MemoryCache(new MemoryCacheOptions()));

        public DistributedCacheInputRemovalPipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterDistributedCacheInputRemovalPipe()
                .Services
                .AddSingleton<IDistributedCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestCacheDoesNotExist()
        {
            var entity = new Entity();
            var result = await new Source<Entity>(entity, this.provider)
                .RemoveFromDistributedCache(Key)
                .ToObjectResultOrDefault();
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestRemovingCacheEntry()
        {
            await this.cache.SetAsync(Key, Encoding.UTF8.GetBytes("test"));
            await new Source<Entity>(new Entity(), this.provider)
                .RemoveFromDistributedCache(Key)
                .ToMockResultPipe()
                .Execute();
            var entry = await this.cache.GetAsync(Key);
            Assert.Null(entry);
        }
    }
}
