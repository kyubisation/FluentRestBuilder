// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Update
{
    using EntityFrameworkCore.Pipes.Update;
    using Microsoft.EntityFrameworkCore;

    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityUpdatePipeFactory(DbContext context)
        {
            this.context = context;
        }

        public EntityUpdatePipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.context, parent);
    }
}