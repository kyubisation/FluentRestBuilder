// <copyright file="ReloadPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class ReloadPipe<TInput> : ChainPipe<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public ReloadPipe(
            IScopedStorage<DbContext> contextStorage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.contextStorage.Value.Entry(input).ReloadAsync();
            return await base.Execute(input);
        }
    }
}
