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
    using Microsoft.Extensions.Logging;

    public class InputEntryAccessPipeFactory<TInput> : IInputEntryAccessPipeFactory<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly ILogger<InputEntryAccessPipe<TInput>> logger;

        public InputEntryAccessPipeFactory(
            IScopedStorage<DbContext> contextStorage,
            ILogger<InputEntryAccessPipe<TInput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Func<EntityEntry<TInput>, Task> entryAction, IOutputPipe<TInput> parent) =>
            new InputEntryAccessPipe<TInput>(
                entryAction, this.contextStorage, this.logger, parent);
    }
}