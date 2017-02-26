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
        public static IFluentRestBuilderCore RegisterActionPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IActionPipeFactory<>), typeof(ActionPipeFactory<>));
            return builder;
        }

        /// <summary>
        /// Perform an action with the given input.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="action">The action to be executed.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Action<TInput> action)
            where TInput : class
        {
            Func<TInput, Task> asyncAction = entity =>
            {
                action(entity);
                return Task.FromResult(0);
            };
            return pipe.Do(asyncAction);
        }

        /// <summary>
        /// Perform an asynchronous action with the given input.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="action">The action to be executed.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> Do<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task> action)
            where TInput : class
        {
            var factory = pipe.GetService<IActionPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(ActionPipe<>));
            return factory.Create(action, pipe);
        }
    }
}
