// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityInsertionPipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.contextStorage, parent);
    }
}