// <copyright file="MemoryCacheActionResultStoragePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.Pipes.MemoryCacheActionResultStorage
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Sources.Source;
    using Xunit;

    public class MemoryCacheActionResultStoragePipeTest
    {
        private const string Key = "key";
        private readonly IServiceProvider provider;
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public MemoryCacheActionResultStoragePipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterMemoryCacheActionResultStoragePipe()
                .Services
                .AddSingleton<IMemoryCache>(p => this.cache)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestStoringResult()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreActionResultInMemoryCache(Key)
                .ToMockResultPipe()
                .Execute();
            var cachedActionResult = this.cache.Get<IActionResult>(Key);
            Assert.Equal(result, cachedActionResult);
        }

        [Fact]
        public async Task TestStoringEntityWithOptions()
        {
            var entity = new Entity { Id = 3 };
            var result = await new Source<Entity>(entity, this.provider)
                .StoreActionResultInMemoryCache(Key, new MemoryCacheEntryOptions())
                .ToMockResultPipe()
                .Execute();
            var cachedActionResult = this.cache.Get<IActionResult>(Key);
            Assert.Equal(result, cachedActionResult);
        }
    }
}
