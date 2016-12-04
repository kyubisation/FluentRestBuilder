// <copyright file="EntityDeletionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Deletion
{
    using Microsoft.EntityFrameworkCore;

    public class EntityDeletionPipeFactory<TInput> : IEntityDeletionPipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityDeletionPipeFactory(DbContext context)
        {
            this.context = context;
        }

        public EntityDeletionPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityDeletionPipe<TInput>(this.context, parent);
    }
}