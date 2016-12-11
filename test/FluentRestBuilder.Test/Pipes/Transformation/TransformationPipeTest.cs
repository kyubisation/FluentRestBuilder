// <copyright file="TransformationPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Transformation
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes.Transformation;
    using Mocks;
    using Xunit;

    public class TransformationPipeTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestTransformation()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var resultPipe = MockSourcePipe<Entity>.CreateCompleteChain(
                entity,
                this.ServiceProvider,
                source => new TransformationPipe<Entity, string>(e => e.Name, source));
            await resultPipe.Execute();
            Assert.Equal(entity.Name, resultPipe.Input);
        }
    }
}
