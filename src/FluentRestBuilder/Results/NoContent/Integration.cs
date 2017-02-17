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
    using Results.NoContent;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterNoContentResult(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(INoContentResultFactory<>), typeof(NoContentResultFactory<>));
            return builder;
        }

        /// <summary>
        /// Executes the pipe chain and returns
        /// a no content action result on success.
        /// (Does not wrap the resulting input.)
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToNoContentResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetService<INoContentResultFactory<TInput>>()
                .Create(pipe)
                .Execute();
    }
}
