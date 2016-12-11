// <copyright file="IEntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.Deletion
{
    using Core;

    public interface IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
