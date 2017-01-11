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
    using Results.CreatedEntity;
    using Storage;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterCreatedEntityResult(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ICreatedEntityResultFactory<>), typeof(CreatedEntityResultFactory<>));
            return builder;
        }

        public static Task<IActionResult> ToCreatedAtRouteResult<TInput>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TInput, object> routeValuesGenerator)
            where TInput : class
        {
            IPipe createdEntityResultPipe = pipe.GetService<ICreatedEntityResultFactory<TInput>>()
                .Resolve(routeValuesGenerator, routeName, pipe);
            return createdEntityResultPipe.Execute();
        }

        public static Task<IActionResult> ToCreatedAtRouteResult<TInput, TLookup>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TLookup, object> routeValuesGenerator)
            where TInput : class
        {
            var storage = pipe.GetService<IScopedStorage<TLookup>>();
            return pipe.ToCreatedAtRouteResult(
                routeName, s => routeValuesGenerator(storage.Value));
        }
    }
}
