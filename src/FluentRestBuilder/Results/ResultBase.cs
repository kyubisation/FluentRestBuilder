// <copyright file="ResultBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results
{
    using System;
    using System.Threading.Tasks;
    using Logger;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public abstract class ResultBase<TInput> : IInputPipe<TInput>
        where TInput : class
    {
        private readonly IOutputPipe<TInput> parent;

        protected ResultBase(
            ILogger logger, IOutputPipe<TInput> parent)
        {
            this.Logger = new LoggerWrapper(logger);
            this.parent = parent;
            this.parent.Attach(this);
        }

        protected LoggerWrapper Logger { get; }

        object IServiceProvider.GetService(Type serviceType) =>
            this.parent.GetService(serviceType);

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            this.Logger.Debug?.Log(
                "Entering method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            this.Logger.Trace?.Log("Received input in pipe {0} is {1}", this.GetType(), input);
            var result = this.CreateResultAsync(input);
            this.Logger.Debug?.Log(
                "Exiting method {0} of pipe {1} with input",
                nameof(IInputPipe<TInput>.Execute),
                this.GetType());
            return result;
        }

        public Task<IActionResult> Execute()
        {
            this.Logger.Debug?.Log(
                "Entering method {0} of pipe {1}", nameof(IPipe.Execute), this.GetType());
            var result = this.parent.Execute();
            this.Logger.Debug?.Log(
                "Exiting method {0} of pipe {1}", nameof(IPipe.Execute), this.GetType());
            this.Logger.Trace?.Log(
                "Resulting {0} is {1}", nameof(IActionResult), result);
            return result;
        }

        protected virtual Task<IActionResult> CreateResultAsync(TInput source) =>
            Task.FromResult(this.CreateResult(source));

        protected virtual IActionResult CreateResult(TInput source)
        {
            throw new NotImplementedException();
        }
    }
}
