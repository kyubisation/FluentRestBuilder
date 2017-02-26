// <copyright file="ActionResultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class ActionResultPipe<TInput> : ChainPipe<TInput>
        where TInput : class
    {
        protected ActionResultPipe(ILogger logger, IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            var result = await this.GenerateActionResult(input);
            if (result != null)
            {
                return result;
            }

            return await base.Execute(input);
        }

        protected virtual Task<IActionResult> GenerateActionResult(TInput entity) =>
            Task.FromResult<IActionResult>(null);
    }
}
