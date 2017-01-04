// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Builder;
    using EntityFrameworkCore.Pipes.QueryableSource;
    using EntityFrameworkCore.QueryableFactories;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterQueryableSourcePipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(IQueryableSourcePipeFactory<,>), typeof(QueryableSourcePipeFactory<,>));
            return builder;
        }

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
