// <copyright file="InputEntryAccessPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.InputEntryAccess
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class InputEntryAccessPipe<TInput> : ChainPipe<TInput>
        where TInput : class
    {
        private readonly Func<EntityEntry<TInput>, Task> entryAction;
        private readonly IScopedStorage<DbContext> contextStorage;

        public InputEntryAccessPipe(
            Func<EntityEntry<TInput>, Task> entryAction,
            IScopedStorage<DbContext> contextStorage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.entryAction = entryAction;
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.ExecuteAction(input);
            return await base.Execute(input);
        }

        private async Task ExecuteAction(TInput input)
        {
            var entry = this.contextStorage.Value.Entry(input);
            await this.entryAction(entry);
        }
    }
}
