// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.FilterByClientRequest;
    using Pipes.FilterByClientRequest.Expressions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterFilterByClientRequestPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IFilterByClientRequestPipeFactory<>),
                typeof(FilterByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IFilterByClientRequestInterpreter, FilterByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            builder.Services.TryAddTransient(
                typeof(IFilterExpressionBuilder<>), typeof(FilterExpressionBuilder<>));
            return builder;
        }

        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                IDictionary<string, IFilterExpressionProvider<TInput>> filterExpressionProviders) =>
            pipe.GetService<IFilterByClientRequestPipeFactory<TInput>>()
                .Resolve(filterExpressionProviders, pipe);

        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<IFilterExpressionBuilder<TInput>, IFilterExpressionBuilder<TInput>> builder)
        {
            var filterExpressionBuilder = pipe.GetService<IFilterExpressionBuilder<TInput>>();
            return pipe.ApplyFilterByClientRequest(builder(filterExpressionBuilder).Build());
        }
    }
}
