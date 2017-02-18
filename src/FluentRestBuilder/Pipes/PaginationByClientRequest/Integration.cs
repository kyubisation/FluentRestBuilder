// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.FilterByClientRequest;
    using Pipes.OrderByClientRequest;
    using Pipes.PaginationByClientRequest;
    using Pipes.SearchByClientRequest;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterPaginationByClientRequestPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IPaginationByClientRequestPipeFactory<>),
                typeof(PaginationByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        /// <summary>
        /// Configure the pagination capabilities for this pipe chain.
        ///
        /// WARNING: Do not use this pipe before the
        /// <see cref="FilterByClientRequestPipe{TInput}"/>, the
        /// <see cref="SearchByClientRequestPipe{TInput}"/> or the
        /// <see cref="OrderByClientRequestPipe{TInput}"/>! This would result in
        /// erroneous pagination logic.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="options">The pagination options.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyPaginationByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                PaginationOptions options = null) =>
            pipe.GetService<IPaginationByClientRequestPipeFactory<TInput>>()
                .Create(options, pipe);
    }
}
