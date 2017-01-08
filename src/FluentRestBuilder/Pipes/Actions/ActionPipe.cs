// <copyright file="ActionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class ActionPipe<TInput> : ChainPipe<TInput>
        where TInput : class
    {
        private readonly Func<TInput, Task> action;

        public ActionPipe(
            Func<TInput, Task> action,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.action = action;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            await this.action(input);
            return await base.Execute(input);
        }
    }
}
