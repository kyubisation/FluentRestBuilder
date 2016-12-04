// <copyright file="InputOutputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Common
{
    using System.Threading.Tasks;
    using Core.Common;
    using Microsoft.AspNetCore.Mvc;

    public abstract class InputOutputPipe<TInput> :
        InputPipe<TInput>,
        IInputPipe<TInput>,
        IOutputPipe<TInput>
        where TInput : class
    {
        private IInputPipe<TInput> child;

        protected InputOutputPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
        }

        async Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            var result = await this.ExecuteAsync(input);
            if (result != null)
            {
                return result;
            }

            NoPipeAttachedException.Check(this.child);
            return await this.child.Execute(input);
        }

        TPipe IOutputPipe<TInput>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        protected virtual Task<IActionResult> ExecuteAsync(TInput entity) =>
            Task.FromResult(this.Execute(entity));

        protected virtual IActionResult Execute(TInput entity) => null;
    }
}
