// <copyright file="ActionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ActionPipeFactory<TInput> : IActionPipeFactory<TInput>
        where TInput : class
    {
        private readonly ILogger<ActionPipe<TInput>> logger;

        public ActionPipeFactory(ILogger<ActionPipe<TInput>> logger = null)
        {
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(Func<TInput, Task> action, IOutputPipe<TInput> parent) =>
            new ActionPipe<TInput>(action, this.logger, parent);
    }
}