// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;

    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityUpdatePipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.contextStorage, parent);
    }
}