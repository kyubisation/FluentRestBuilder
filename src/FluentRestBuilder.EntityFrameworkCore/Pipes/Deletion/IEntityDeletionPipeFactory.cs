// <copyright file="IEntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    public interface IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
