// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Sources.Source;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterSource(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ISourceFactory<>), typeof(SourceFactory<>));
            return builder;
        }

        /// <summary>
        /// Create a source pipe with the given value. Awaits the given value.
        /// </summary>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="output">The output value.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> FromSource<TOutput>(
                this ControllerBase controller, Task<TOutput> output) =>
            controller.HttpContext.RequestServices.GetService<ISourceFactory<TOutput>>()
                .Create(output, controller);

        /// <summary>
        /// Create a source pipe with the given value.
        /// </summary>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="output">The output value.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> FromSource<TOutput>(
            this ControllerBase controller, TOutput output) =>
            controller.FromSource(Task.FromResult(output));
    }
}
