// <copyright file="BaseSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseSourcePipe<TOutput> : OutputPipe<TOutput>
    {
        protected BaseSourcePipe(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        protected override async Task<IActionResult> Execute()
        {
            NoPipeAttachedException.Check(this.Child);
            return await this.Child.Execute(await this.GetOutput());
        }

        protected abstract Task<TOutput> GetOutput();
    }
}
