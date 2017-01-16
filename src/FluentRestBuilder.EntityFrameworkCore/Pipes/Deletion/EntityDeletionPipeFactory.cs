// <copyright file="EntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public EntityDeletionPipeFactory(IDbContextContainer dbContextContainer)
        {
            this.dbContextContainer = dbContextContainer;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.dbContextContainer, parent);
    }
}