// <copyright file="ChainPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ChainPipe<TInput> : OutputPipe<TInput>, IInputPipe<TInput>
    {
        private readonly IOutputPipe<TInput> parent;

        protected ChainPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input) => this.Execute(input);

        protected virtual async Task<IActionResult> Execute(TInput input) =>
            await this.ExecuteChild(input);

        protected override Task<IActionResult> Execute() => this.parent.Execute();

        protected virtual async Task<IActionResult> ExecuteChild(TInput input)
        {
            NoPipeAttachedException.Check(this.Child);
            return await this.Child.Execute(input);
        }
    }
}
