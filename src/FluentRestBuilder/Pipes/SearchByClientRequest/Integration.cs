// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.SearchByClientRequest;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> ApplySearchByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<string, Expression<Func<TInput, bool>>> search) =>
            pipe.GetService<ISearchByClientRequestPipeFactory<TInput>>()
                .Resolve(search, pipe);
    }
}
