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