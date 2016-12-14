// <copyright file="InputOutputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public abstract class InputOutputPipe<TInput> :
        OutputPipe<TInput>,
        IInputPipe<TInput>
        where TInput : class
    {
        private readonly IOutputPipe<TInput> parent;

        protected InputOutputPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        async Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            var result = await this.ExecuteAsync(input);
            if (result != null)
            {
                return result;
            }

            NoPipeAttachedException.Check(this.Child);
            return await this.Child.Execute(input);
        }

        protected virtual Task<IActionResult> ExecuteAsync(TInput entity) =>
            Task.FromResult(this.Execute(entity));

        protected virtual IActionResult Execute(TInput entity) => null;

        protected override Task<IActionResult> Execute() => this.parent.Execute();
    }
}
