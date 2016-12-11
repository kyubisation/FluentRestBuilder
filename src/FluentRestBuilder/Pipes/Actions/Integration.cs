 // <copyright file="Integration.cs" company="Kyubisation">
 // Copyright (c) Kyubisation. All rights reserved.
 // </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder;
    using FluentRestBuilder.Pipes.Actions;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class =>
            pipe.GetService<IActionPipeFactory<TInput>>().Resolve(action, pipe);

        public static ActionPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class =>
            pipe.GetService<IActionPipeFactory<TInput>>().Resolve(action, pipe);
    }
}
