// <copyright file="LazySourceTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Sources.LazySource;
    using Xunit;

    public class LazySourceTest
    {
        [Fact]
        public async Task TestNoChildAttached()
        {
            var sourcePipe = new LazySource<Entity>(() => new Entity(), new EmptyServiceProvider());
            await Assert.ThrowsAsync<NoPipeAttachedException>(() => ((IOutputPipe<Entity>)sourcePipe).Execute());
        }

        [Fact]
        public async Task TestExecute()
        {
            var lazySource = new Lazy<Entity>(() => new Entity());
            var resultPipe = new LazySource<Entity>(() => lazySource.Value, new EmptyServiceProvider())
                .ToMockResultPipe();
            Assert.False(lazySource.IsValueCreated);
            var result = await resultPipe.Execute()
                .GetObjectResultOrDefault<Entity>();
            Assert.True(lazySource.IsValueCreated);
            Assert.Same(lazySource.Value, result);
        }
    }
}
