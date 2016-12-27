// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.OrderByClientRequest;
    using Pipes.OrderByClientRequest.Expressions;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> ApplyOrderBy<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<IOrderByExpressionBuilder<TInput>, IDictionary<string, IOrderByExpressionFactory<TInput>>> builder)
        {
            var orderByExpressionBuilder = pipe.GetService<IOrderByExpressionBuilder<TInput>>();
            return pipe.GetService<IOrderByClientRequestPipeFactory<TInput>>()
                .Resolve(builder(orderByExpressionBuilder), pipe);
        }
    }
}
