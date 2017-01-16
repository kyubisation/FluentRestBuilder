// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public EntityUpdatePipeFactory(IDbContextContainer dbContextContainer)
        {
            this.dbContextContainer = dbContextContainer;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.dbContextContainer, parent);
    }
}