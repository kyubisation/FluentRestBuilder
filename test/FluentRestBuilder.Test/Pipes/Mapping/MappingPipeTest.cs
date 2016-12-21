// <copyright file="MappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Mapping
{
    using System.Threading.Tasks;
    using Common.Mocks;
    using FluentRestBuilder.Pipes.Mapping;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MappingPipeTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestTransformation()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var provider = new ServiceCollection()
                .AddTransient<IMappingPipeFactory<Entity, string>, MappingPipeFactory<Entity, string>>()
                .BuildServiceProvider();
            var result = await new SourcePipe<Entity>(entity, provider)
                .Map(e => e.Name)
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Name, result);
        }
    }
}
