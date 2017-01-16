// <copyright file="EntityInsertionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    public class EntityInsertionPipeFactory<TInput> : IEntityInsertionPipeFactory<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public EntityInsertionPipeFactory(IDbContextContainer dbContextContainer)
        {
            this.dbContextContainer = dbContextContainer;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityInsertionPipe<TInput>(this.dbContextContainer, parent);
    }
}