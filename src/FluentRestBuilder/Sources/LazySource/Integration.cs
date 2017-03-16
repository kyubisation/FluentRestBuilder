// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sources.LazySource;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterLazySource(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ILazySourceFactory<>), typeof(LazySourceFactory<>));
            return builder;
        }

        /// <summary>
        /// Create a source pipe with the given asynchronous factory function.
        /// The factory function is called, when the pipe execution reaches this pipe.
        /// </summary>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="output">The output factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> FromSource<TOutput>(
                this ControllerBase controller, Func<Task<TOutput>> output) =>
            controller.HttpContext.RequestServices.GetService<ILazySourceFactory<TOutput>>()
                .Create(output, controller);

        /// <summary>
        /// Create a source pipe with the given factory function.
        /// The factory function is called, when the pipe execution reaches this pipe.
        /// </summary>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="output">The output factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, Func<TOutput> output) =>
            controller.FromSource(() => Task.FromResult(output()));
    }
}
