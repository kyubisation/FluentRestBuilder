// <copyright file="IEntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Deletion
{
    using EntityFrameworkCore.Pipes.Deletion;

    public interface IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
