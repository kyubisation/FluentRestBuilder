// <copyright file="InputEntryAccessPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.InputEntryAccess
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class InputEntryAccessPipeFactory<TInput> : IInputEntryAccessPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public InputEntryAccessPipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TInput> Create(
            Func<EntityEntry<TInput>, Task> entryAction, IOutputPipe<TInput> parent) =>
            new InputEntryAccessPipe<TInput>(entryAction, this.contextStorage, parent);
    }
}