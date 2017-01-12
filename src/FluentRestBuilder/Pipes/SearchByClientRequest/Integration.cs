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
        public static IFluentRestBuilderCore RegisterSearchByClientRequestPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(ISearchByClientRequestPipeFactory<>),
                typeof(SearchByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            return builder;
        }

        public static OutputPipe<IQueryable<TInput>> ApplySearchByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<string, Expression<Func<TInput, bool>>> search) =>
            pipe.GetService<ISearchByClientRequestPipeFactory<TInput>>()
                .Create(search, pipe);
    }
}
