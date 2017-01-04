// <copyright file="ReloadPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;

    public class ReloadPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly IContextActions contextActions;

        public ReloadPipe(
            IContextActions contextActions,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.contextActions = contextActions;
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            await this.contextActions.Reload(entity);
            return await base.ExecuteAsync(entity);
        }
    }
}
