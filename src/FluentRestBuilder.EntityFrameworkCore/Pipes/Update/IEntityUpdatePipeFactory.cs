// <copyright file="IEntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    public interface IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(IOutputPipe<TInput> parent);
    }
}
