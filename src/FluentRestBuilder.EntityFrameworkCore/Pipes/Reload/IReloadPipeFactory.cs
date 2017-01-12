// <copyright file="IReloadPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    public interface IReloadPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(IOutputPipe<TInput> parent);
    }
}
