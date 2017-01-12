// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Results.Options;
    using Storage;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterOptionsResultPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IOptionsResultFactory<>),
                typeof(OptionsResultFactory<>));
            builder.Services.TryAddScoped(
                typeof(IAllowedOptionsBuilder<>),
                typeof(AllowedOptionsBuilder<>));
            builder.Services.TryAddSingleton<IHttpVerbMap, HttpVerbMap>();
            return builder;
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<IAllowedOptionsBuilder<TInput>, IAllowedOptionsBuilder<TInput>> builder)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            IPipe resultPipe = pipe.GetService<IOptionsResultFactory<TInput>>()
                .Resolve(
                    input => builder(allowedOptionsBuilder).GenerateAllowedVerbs(input), pipe);
            return resultPipe.Execute();
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            params HttpVerb[] verbs)
            where TInput : class
        {
            IPipe resultPipe = pipe.GetService<IOptionsResultFactory<TInput>>()
                .Resolve(input => verbs, pipe);
            return resultPipe.Execute();
        }
    }
}
