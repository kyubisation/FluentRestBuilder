// <copyright file="MemoryCacheInputRemovalPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.MemoryCacheInputRemoval
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

    public class MemoryCacheInputRemovalPipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public MemoryCacheInputRemovalPipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterMemoryCacheInputRemovalPipe()
                .Services
                .AddSingleton<IMemoryCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestCacheDoesNotExist()
        {
            var entity = new Entity();
            var result = await new Source<Entity>(entity, this.provider)
                .RemoveFromMemoryCache(Key)
                .ToObjectResultOrDefault();
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public async Task TestRemovingCacheEntry()
        {
            this.cache.Set(Key, "test");
            await new Source<Entity>(new Entity(), this.provider)
                .RemoveFromMemoryCache(Key)
                .ToMockResultPipe()
                .Execute();
            object value;
            Assert.False(this.cache.TryGetValue(Key, out value));
        }
    }
}
