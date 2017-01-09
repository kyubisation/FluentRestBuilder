// <copyright file="TestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Builder.NestedFactoryRegistrationTestMocks
{
    using FluentRestBuilder.Pipes;

    public class TestPipe<TInput> : ChainPipe<TInput>
    {
        public TestPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        public class Factory : IPipeFactory<TInput>
        {
            public OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
                new TestPipe<TInput>(parent);
        }
    }
}
