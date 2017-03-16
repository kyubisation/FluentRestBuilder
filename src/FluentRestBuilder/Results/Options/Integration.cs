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
    using Results.Options;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterOptionsResultPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IOptionsResultFactory<>),
                typeof(OptionsResultFactory<>));
            builder.Services.TryAddScoped(
                typeof(IAllowedOptionsBuilder<>),
                typeof(AllowedOptionsBuilder<>));
            builder.Services.TryAddSingleton<IHttpVerbDictionary, HttpVerbDictionary>();
            return builder;
        }

        /// <summary>
        /// Executes the pipe chain and creates an empty ok action result.
        /// The builder generates the allowed HTTP verbs and sets the Allow header.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="builder">The allowed options builder.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<IAllowedOptionsBuilder<TInput>, IAllowedOptionsBuilder<TInput>> builder)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            return pipe.GetService<IOptionsResultFactory<TInput>>()
                .Create(input => builder(allowedOptionsBuilder).GenerateAllowedVerbs(input), pipe)
                .Execute();
        }

        /// <summary>
        /// Executes the pipe chain and creates an empty ok action result.
        /// The given HTTP verbs will be set as the Allow header.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="verbs">The allowed HTTP verbs.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            params HttpVerb[] verbs)
            where TInput : class =>
            pipe.GetService<IOptionsResultFactory<TInput>>()
                .Create(input => verbs, pipe)
                .Execute();
    }
}
