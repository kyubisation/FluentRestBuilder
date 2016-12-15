// <copyright file="MappingPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Mapping
{
    using System.Threading.Tasks;
    using Common.Mocks;
    using FluentRestBuilder.Pipes.Mapping;
    using Xunit;

    public class MappingPipeTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestTransformation()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                source => new MappingPipe<Entity, string>(e => e.Name, source));
            await resultPipe.Execute();
            Assert.Equal(entity.Name, resultPipe.Input);
        }
    }
}
