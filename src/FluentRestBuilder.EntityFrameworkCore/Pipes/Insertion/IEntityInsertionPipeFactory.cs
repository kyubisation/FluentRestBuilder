// <copyright file="IEntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    public interface IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(IOutputPipe<TInput> parent);
    }
}
