// <copyright file="IEntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Insertion
{
    public interface IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        EntityInsertionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
