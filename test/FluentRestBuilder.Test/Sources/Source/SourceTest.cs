// <copyright file="SourceTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Sources.Source
{
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class SourceTest
    {
        [Fact]
        public async Task TestNoChildAttached()
        {
            var sourcePipe = new Source<Entity>(new Entity(), new EmptyServiceProvider());
            await Assert.ThrowsAsync<NoPipeAttachedException>(
                () => ((IOutputPipe<Entity>)sourcePipe).Execute());
        }

        [Fact]
        public async Task TestExecute()
        {
            var source = new Entity();
            var result = await new Source<Entity>(source, new EmptyServiceProvider())
                .ToObjectResultOrDefault();
            Assert.Same(source, result);
        }

        [Fact]
        public async Task TestFromController()
        {
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .Services
                .BuildServiceProvider();
            using (var controller = new MockController(provider))
            {
                var entity = new Entity();
                var result = await controller.FromSource(entity)
                    .ToObjectResultOrDefault();
                Assert.Same(entity, result);
            }
        }
    }
}
