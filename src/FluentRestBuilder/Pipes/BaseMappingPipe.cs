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
        private readonly Func<TInput, TOutput> mapping;

        protected BaseMappingPipe(
            Func<TInput, TOutput> mapping, IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.mapping = mapping;
            this.parent = parent;
            this.parent.Attach(this);
        }

        async Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            NoPipeAttachedException.Check(this.Child);
            var result = this.mapping(input);
            return await this.Child.Execute(result);
        }

        protected override Task<IActionResult> Execute() => this.parent.Execute();
    }
}
