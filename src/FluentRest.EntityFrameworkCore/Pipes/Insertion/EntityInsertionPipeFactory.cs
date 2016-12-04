// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Insertion
{
    using EntityFrameworkCore.Pipes.Insertion;
    using Microsoft.EntityFrameworkCore;

    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityInsertionPipeFactory(DbContext context)
        {
            this.context = context;
        }

        public EntityInsertionPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.context, parent);
    }
}