// <copyright file="IActionPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Actions
{
    using System;
    using System.Threading.Tasks;

    public interface IActionPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(Func<TInput, Task> action, IOutputPipe<TInput> parent);
    }
}
