// <copyright file="SourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Sources.Source
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class SourcePipe<TOutput> : IOutputPipe<TOutput>
    {
        private readonly Task<TOutput> output;
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<TOutput> child;

        public SourcePipe(Task<TOutput> output, IServiceProvider serviceProvider)
        {
            this.output = output;
            this.serviceProvider = serviceProvider;
        }

        public SourcePipe(TOutput output, IServiceProvider serviceProvider)
            : this(Task.FromResult(output), serviceProvider)
        {
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
            return await this.child.Execute(await this.output);
        }
    }
}
