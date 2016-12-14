// <copyright file="BaseMappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseMappingPipe<TInput, TOutput> : OutputPipe<TOutput>, IInputPipe<TInput>
    {
        private readonly IOutputPipe<TInput> parent;

        protected BaseMappingPipe(IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input) => this.Execute(input);

        protected virtual async Task<IActionResult> Execute(TInput input)
        {
            NoPipeAttachedException.Check(this.Child);
            var result = await this.MapAsync(input);
            return await this.Child.Execute(result);
        }

        protected override Task<IActionResult> Execute() => this.parent.Execute();

        protected virtual Task<TOutput> MapAsync(TInput input) =>
            Task.FromResult(this.Map(input));

        protected virtual TOutput Map(TInput input)
        {
            throw new NotImplementedException();
        }
    }
}
