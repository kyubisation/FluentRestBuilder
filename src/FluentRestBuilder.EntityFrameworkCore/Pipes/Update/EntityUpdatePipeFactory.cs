// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IScopedStorage<TInput> entityStorage;

        public EntityUpdatePipeFactory(DbContext context, IScopedStorage<TInput> entityStorage)
        {
            this.context = context;
            this.entityStorage = entityStorage;
        }

        public EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.context, this.entityStorage, parent);
    }
}