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
    using Results.Accepted;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterAcceptedResult(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IAcceptedResultFactory<>), typeof(AcceptedResultFactory<>));
            return builder;
        }

        /// <summary>
        /// Executes the pipe chain and wraps the result in
        /// an accepted action result on success.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToAcceptedResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class =>
            pipe.GetService<IAcceptedResultFactory<TInput>>()
                .Create(pipe)
                .Execute();
    }
}
