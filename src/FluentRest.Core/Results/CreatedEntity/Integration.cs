// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRest
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Core.Results.CreatedEntity;
    using Core.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static Task<IActionResult> ToCreatedAtRouteResult<TInput, TLookup>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TLookup, object> routeValuesGenerator)
            where TInput : class
        {
            IPipe createdEntityResultPipe = new CreatedEntityResultPipe<TInput>(
                s =>
                {
                    var storage = s.GetService<IScopedStorage<TLookup>>();
                    return routeValuesGenerator(storage.Value);
                },
                routeName,
                pipe);
            return createdEntityResultPipe.Execute();
        }

        public static Task<IActionResult> ToCreatedAtRouteResult<TInput>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TInput, object> routeValuesGenerator)
            where TInput : class
        {
            IPipe createdEntityResultPipe = new CreatedEntityResultPipe<TInput>(
                routeValuesGenerator, routeName, pipe);
            return createdEntityResultPipe.Execute();
        }
    }
}
