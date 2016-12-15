// <copyright file="SourcePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Sources.Source
{
    using System.Threading.Tasks;
    using Common.Mocks;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SourcePipeTest
    {
        [Fact]
        public async Task TestNoChildAttached()
        {
            var sourcePipe = new SourcePipe<Entity>(new Entity(), new ServiceCollection().BuildServiceProvider());
            await Assert.ThrowsAsync<NoPipeAttachedException>(() => ((IOutputPipe<Entity>)sourcePipe).Execute());
        }

        [Fact]
        public async Task TestExecute()
        {
            var source = new Entity();
            var sourcePipe = new SourcePipe<Entity>(source, new ServiceCollection().BuildServiceProvider());
            var resultPipe = new MockResultPipe<Entity>(sourcePipe);
            await resultPipe.Execute();
            Assert.Same(source, resultPipe.Input);
        }
    }
}
