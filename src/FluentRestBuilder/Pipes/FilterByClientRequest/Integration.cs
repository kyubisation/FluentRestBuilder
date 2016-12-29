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
    using Pipes.FilterByClientRequest;
    using Pipes.FilterByClientRequest.Expressions;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<IFilterExpressionBuilder<TInput>, IDictionary<string, IFilterExpressionProvider<TInput>>> builder)
        {
            var filterExpressionBuilder = pipe.GetService<IFilterExpressionBuilder<TInput>>();
            return pipe.GetService<IFilterByClientRequestPipeFactory<TInput>>()
                .Resolve(builder(filterExpressionBuilder), pipe);
        }
    }
}
