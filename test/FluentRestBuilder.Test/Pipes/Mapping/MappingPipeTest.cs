// <copyright file="MappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Mapping
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MappingPipeTest
    {
        private readonly IServiceProvider provider;

        public MappingPipeTest()
        {
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterMappingPipe()
                .Services
                .BuildServiceProvider();
        }

        [Fact]
        public async Task TestMap()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var result = await new Source<Entity>(entity, this.provider)
                .Map(e => e.Name)
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Name, result);
        }
    }
}
