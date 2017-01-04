// <copyright file="EntityUpdatePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    public class EntityUpdatePipeFactory<TInput> : IEntityUpdatePipeFactory<TInput>
        where TInput : class
    {
        private readonly IContextActions contextActions;

        public EntityUpdatePipeFactory(IContextActions contextActions)
        {
            this.contextActions = contextActions;
        }

        public OutputPipe<TInput> Resolve(IOutputPipe<TInput> parent) =>
            new EntityUpdatePipe<TInput>(this.contextActions, parent);
    }
}