// <copyright file="OutputPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Logger;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class OutputPipe<TOutput> : IOutputPipe<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        protected OutputPipe(
            ILogger logger,
            IServiceProvider serviceProvider)
        {
            this.Logger = new LoggerWrapper(logger);
            this.serviceProvider = serviceProvider;
        }

        protected LoggerWrapper Logger { get; }

        protected IInputPipe<TOutput> Child { get; private set; }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        TPipe IOutputPipe<TOutput>.Attach<TPipe>(TPipe pipe)
        {
            this.Child = pipe;
            this.Logger.Debug?.Log(
                "Attaching pipe {0} to pipe {1}", pipe.GetType(), this.GetType());
            return pipe;
        }

        Task<IActionResult> IPipe.Execute()
        {
            this.Logger.Debug?.Log(
                "Entering method {0} of pipe {1}", nameof(IPipe.Execute), this.GetType());
            var result = this.Execute();
            this.Logger.Debug?.Log(
                "Exiting method {0} of pipe {1}", nameof(IPipe.Execute), this.GetType());
            return result;
        }

        protected abstract Task<IActionResult> Execute();
    }
}
