// <copyright file="LazySourcePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Sources.LazySource
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Sources.LazySource;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class LazySourcePipeTest
    {
        [Fact]
        public async Task TestNoChildAttached()
        {
            var sourcePipe = new LazySourcePipe<Entity>(() => new Entity(), new ServiceCollection().BuildServiceProvider());
            await Assert.ThrowsAsync<NoPipeAttachedException>(() => ((IOutputPipe<Entity>)sourcePipe).Execute());
        }

        [Fact]
        public async Task TestExecute()
        {
            var source = new Entity();
            var sourcePipe = new LazySourcePipe<Entity>(() => source, new ServiceCollection().BuildServiceProvider());
            var resultPipe = new MockResultPipe<Entity>(sourcePipe);
            await resultPipe.Execute();
            Assert.Same(source, resultPipe.Input);
        }
    }
}
