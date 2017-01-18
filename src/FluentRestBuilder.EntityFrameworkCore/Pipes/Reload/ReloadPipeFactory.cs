// <copyright file="ReloadPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class ReloadPipeFactory<TInput> : IReloadPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public ReloadPipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TInput> Create(IOutputPipe<TInput> parent) =>
            new ReloadPipe<TInput>(this.contextStorage, parent);
    }
}