// <copyright file="ActionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;

    public class ActionPipeFactory<TInput> : IActionPipeFactory<TInput>
        where TInput : class
    {
        public ActionPipe<TInput> Resolve(Func<TInput, Task> action, IOutputPipe<TInput> parent) =>
            new ActionPipe<TInput>(action, parent);

        public ActionPipe<TInput> Resolve(Action<TInput> action, IOutputPipe<TInput> parent) =>
            new ActionPipe<TInput>(action, parent);
    }
}