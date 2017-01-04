// <copyright file="EntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IContextActions contextActions;

        public EntityDeletionPipeFactory(IContextActions contextActions)
        {
            this.contextActions = contextActions;
        }

        public OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.contextActions, parent);
    }
}