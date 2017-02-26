// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly ILogger<EntityUpdatePipe<TInput>> logger;

        public EntityUpdatePipeFactory(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityUpdatePipe<TInput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.contextStorage, this.logger, parent);
    }
}