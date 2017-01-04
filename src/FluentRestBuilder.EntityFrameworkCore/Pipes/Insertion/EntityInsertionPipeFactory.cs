// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    using Storage;

    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<TInput> entityStorage;
        private readonly IContextActions contextActions;

        public EntityInsertionPipeFactory(
            IScopedStorage<TInput> entityStorage, IContextActions contextActions)
        {
            this.entityStorage = entityStorage;
            this.contextActions = contextActions;
        }

        public OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.contextActions, this.entityStorage, parent);
    }
}