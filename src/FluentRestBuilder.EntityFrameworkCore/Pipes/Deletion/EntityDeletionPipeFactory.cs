// <copyright file="EntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;

    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityDeletionPipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.contextStorage, parent);
    }
}