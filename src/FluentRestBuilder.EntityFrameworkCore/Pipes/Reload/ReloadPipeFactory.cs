// <copyright file="ReloadPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    public class ReloadPipeFactory<TInput> : IReloadPipeFactory<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public ReloadPipeFactory(IDbContextContainer dbContextContainer)
        {
            this.dbContextContainer = dbContextContainer;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new ReloadPipe<TInput>(this.dbContextContainer, parent);
    }
}