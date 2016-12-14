// <copyright file="IEntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    public interface IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
