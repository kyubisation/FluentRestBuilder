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
    using Pipes.OrderByClientRequest;
    using Pipes.OrderByClientRequest.Expressions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterOrderByClientRequestPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IOrderByClientRequestPipeFactory<>),
                typeof(OrderByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IOrderByClientRequestInterpreter, OrderByClientRequestInterpreter>();
            builder.Services.TryAddTransient(
                typeof(IOrderByExpressionBuilder<>), typeof(OrderByExpressionBuilder<>));
            return builder;
        }

        public static OutputPipe<IQueryable<TInput>> ApplyOrderByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                IDictionary<string, IOrderByExpressionFactory<TInput>> orderByExpressions) =>
            pipe.GetService<IOrderByClientRequestPipeFactory<TInput>>()
                .Resolve(orderByExpressions, pipe);

        public static OutputPipe<IQueryable<TInput>> ApplyOrderByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<IOrderByExpressionBuilder<TInput>, IOrderByExpressionBuilder<TInput>> builder)
        {
            var orderByExpressionBuilder = pipe.GetService<IOrderByExpressionBuilder<TInput>>();
            return pipe.ApplyOrderByClientRequest(builder(orderByExpressionBuilder).Build());
        }
    }
}
