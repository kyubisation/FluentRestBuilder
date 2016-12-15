 // <copyright file="Integration.cs" company="Kyubisation">
 // Copyright (c) Kyubisation. All rights reserved.
 // </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.Actions;

    public static partial class Integration
    {
        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class
        {
            Func<TInput, Task> asyncAction = entity =>
            {
                action(entity);
                return Task.CompletedTask;
            };
            return pipe.GetService<IActionPipeFactory<TInput>>().Resolve(asyncAction, pipe);
        }

        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class =>
            pipe.GetService<IActionPipeFactory<TInput>>().Resolve(action, pipe);
    }
}
