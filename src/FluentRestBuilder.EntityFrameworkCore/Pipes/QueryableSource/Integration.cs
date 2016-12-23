// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using EntityFrameworkCore.Common;
    using EntityFrameworkCore.Pipes.QueryableSource;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static QueryableSourcePipe<TInput, TOutput> SelectQueryableSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IQueryableFactory, TInput, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.GetRequiredService<IQueryableSourcePipeFactory<TInput, TOutput>>()
                .Resolve(selection, pipe);

        public static QueryableSourcePipe<TInput, TOutput> SelectQueryableSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IQueryableFactory, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.SelectQueryableSource((queryableFactory, i) => selection(queryableFactory));
    }
}
