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
        public static IFluentRestBuilderCoreConfiguration RegisterCreatedEntityResult(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton(
                typeof(ICreatedEntityResultFactory<>), typeof(CreatedEntityResultFactory<>));
            return builder;
        }

        /// <summary>
        /// Executes the pipe chain and wraps the result in
        /// an created at route action result on success.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="routeName">The route name.</param>
        /// <param name="routeValuesFactory">The route value factory.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToCreatedAtRouteResult<TInput>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TInput, object> routeValuesFactory = null)
            where TInput : class =>
            pipe.GetService<ICreatedEntityResultFactory<TInput>>()
                .Create(routeValuesFactory, routeName, pipe)
                .Execute();

        /// <summary>
        /// Executes the pipe chain and wraps the result in
        /// an created at route action result on success.
        /// Tries to resolve the lookup value from a scoped storage if available.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TLookup">The lookup type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="routeName">The route name.</param>
        /// <param name="routeValuesFactory">The route value factory.</param>
        /// <returns>An asynchronous <see cref="IActionResult"/>.</returns>
        public static Task<IActionResult> ToCreatedAtRouteResult<TInput, TLookup>(
            this IOutputPipe<TInput> pipe,
            string routeName,
            Func<TLookup, object> routeValuesFactory)
            where TInput : class
        {
            var storage = pipe.GetService<IScopedStorage<TLookup>>();
            return pipe.ToCreatedAtRouteResult(
                routeName, s => routeValuesFactory(storage.Value));
        }
    }
}
