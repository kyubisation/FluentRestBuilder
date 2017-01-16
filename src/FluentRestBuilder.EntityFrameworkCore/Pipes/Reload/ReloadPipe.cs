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
        private readonly IDbContextContainer dbContextContainer;

        public ReloadPipe(
            IDbContextContainer dbContextContainer,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.dbContextContainer = dbContextContainer;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.dbContextContainer.Context.Entry(input).ReloadAsync();
            return await base.Execute(input);
        }
    }
}
