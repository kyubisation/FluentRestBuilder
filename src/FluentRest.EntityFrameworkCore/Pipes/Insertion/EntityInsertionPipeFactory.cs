// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.Insertion
{
    using Core;
    using Core.Storage;
    using Microsoft.EntityFrameworkCore;

    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IScopedStorage<TInput> entityStorage;

        public EntityInsertionPipeFactory(DbContext context, IScopedStorage<TInput> entityStorage)
        {
            this.context = context;
            this.entityStorage = entityStorage;
        }

        public EntityInsertionPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.context, this.entityStorage, parent);
    }
}