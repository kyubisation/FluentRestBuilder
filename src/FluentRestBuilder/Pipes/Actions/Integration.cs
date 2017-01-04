 // <copyright file="Integration.cs" company="Kyubisation">
 // Copyright (c) Kyubisation. All rights reserved.
 // </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.Actions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterActionPipe(this IFluentRestBuilder builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IActionPipeFactory<>), typeof(ActionPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class
        {
            Func<TInput, Task> asyncAction = entity =>
            {
                action(entity);
                return Task.FromResult(0);
            };
            return pipe.GetService<IActionPipeFactory<TInput>>().Resolve(asyncAction, pipe);
        }

        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class =>
            pipe.GetService<IActionPipeFactory<TInput>>().Resolve(action, pipe);
    }
}
