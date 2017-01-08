// <copyright file="ReloadPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Reload
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;

    public class ReloadPipe<TInput> : ChainPipe<TInput>
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

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.contextActions.Reload(input);
            return await base.Execute(input);
        }
    }
}
