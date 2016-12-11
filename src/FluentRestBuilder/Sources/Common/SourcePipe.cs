// <copyright file="SourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Common
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class SourcePipe<TOutput> : IOutputPipe<TOutput>
    {
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<TOutput> child;

        protected SourcePipe(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        TPipe IOutputPipe<TOutput>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        async Task<IActionResult> IPipe.Execute()
        {
            NoPipeAttachedException.Check(this.child);
            return await this.child.Execute(await this.GetOutput());
        }

        protected abstract Task<TOutput> GetOutput();
    }
}
