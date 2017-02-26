// <copyright file="ChainPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class ChainPipe<TInput> : OutputPipe<TInput>, IInputPipe<TInput>
    {
        private readonly IOutputPipe<TInput> parent;

        protected ChainPipe(ILogger logger, IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            this.Logger.Debug?.Log(
                "Entering method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            this.Logger.Trace?.Log("Received input in pipe {0} is {1}", this.GetType(), input);
            var result = this.Execute(input);
            this.Logger.Debug?.Log(
                "Exiting method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            return result;
        }

        protected virtual async Task<IActionResult> Execute(TInput input) =>
            await this.ExecuteChild(input);

        protected override Task<IActionResult> Execute() => this.parent.Execute();

        protected virtual async Task<IActionResult> ExecuteChild(TInput input)
        {
            Check.IsPipeAttached(this.Child);
            return await this.Child.Execute(input);
        }
    }
}
