// <copyright file="IEntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Deletion
{
    public interface IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent);
    }
}
