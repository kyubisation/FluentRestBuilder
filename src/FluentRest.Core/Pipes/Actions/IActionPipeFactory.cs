// <copyright file="IActionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;

    public interface IActionPipeFactory<TInput>
        where TInput : class
    {
        ActionPipe<TInput> Resolve(Func<TInput, Task> action, IOutputPipe<TInput> parent);

        ActionPipe<TInput> Resolve(Action<TInput> action, IOutputPipe<TInput> parent);
    }
}
