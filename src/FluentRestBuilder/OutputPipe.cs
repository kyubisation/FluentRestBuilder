// <copyright file="OutputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public abstract class OutputPipe<TOutput> : IOutputPipe<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        protected OutputPipe(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected IInputPipe<TOutput> Child { get; private set; }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        TPipe IOutputPipe<TOutput>.Attach<TPipe>(TPipe pipe)
        {
            this.Child = pipe;
            return pipe;
        }

        Task<IActionResult> IPipe.Execute() => this.Execute();

        protected abstract Task<IActionResult> Execute();
    }
}
