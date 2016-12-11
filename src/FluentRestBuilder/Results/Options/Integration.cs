// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Core.Results.Options;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<IAllowedOptionsBuilder<TInput>, IEnumerable<HttpVerb>> builder)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            var httpContextAccessor = pipe.GetService<IHttpContextAccessor>();
            IPipe resultPipe = new OptionsResultPipe<TInput>(
                input => builder(allowedOptionsBuilder),
                httpContextAccessor,
                pipe);
            return resultPipe.Execute();
        }

        public static Task<IActionResult> ToOptionsResult<TInput>(
            this IOutputPipe<TInput> pipe,
            params HttpVerb[] verbs)
            where TInput : class
        {
            var allowedOptionsBuilder = pipe.GetService<IAllowedOptionsBuilder<TInput>>();
            var httpContextAccessor = pipe.GetService<IHttpContextAccessor>();
            IPipe resultPipe = new OptionsResultPipe<TInput>(
                input => verbs, httpContextAccessor, pipe);
            return resultPipe.Execute();
        }
    }
}
