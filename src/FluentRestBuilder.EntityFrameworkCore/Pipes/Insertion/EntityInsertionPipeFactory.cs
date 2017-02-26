// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly ILogger<EntityInsertionPipe<TInput>> logger;

        public EntityInsertionPipeFactory(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityInsertionPipe<TInput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.contextStorage, this.logger, parent);
    }
}