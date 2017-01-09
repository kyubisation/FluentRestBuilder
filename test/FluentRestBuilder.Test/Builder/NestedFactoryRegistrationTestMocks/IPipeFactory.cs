// <copyright file="IPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Builder.NestedFactoryRegistrationTestMocks
{
    public interface IPipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
