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
    using Results.Ok;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterOkResult(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IOkResultFactory<>), typeof(OkResultFactory<>));
            return builder;
        }

        /// <summary>
        /// Executes the pipe chain and wraps the result in
        /// an ok action result on success.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToOkResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetService<IOkResultFactory<TInput>>()
                .Create(pipe)
                .Execute();
    }
}
