// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.PaginationByClientRequest;
    using Pipes.SearchByClientRequest;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterSearchByClientRequestPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(ISearchByClientRequestPipeFactory<>),
                typeof(SearchByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            return builder;
        }

        /// <summary>
        /// Configure the search capabilities for this pipe chain.
        /// Provide a factory function that searches an input according to a single search value.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="search">The search factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplySearchByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<string, Expression<Func<TInput, bool>>> search)
        {
            var factory = pipe.GetService<ISearchByClientRequestPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(SearchByClientRequestPipe<>));
            return factory.Create(search, pipe);
        }
    }
}
