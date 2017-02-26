// <copyright file="EntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly ILogger<EntityDeletionPipe<TInput>> logger;

        public EntityDeletionPipeFactory(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityDeletionPipe<TInput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.contextStorage, this.logger, parent);
    }
}