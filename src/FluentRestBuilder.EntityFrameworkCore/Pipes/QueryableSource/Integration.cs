// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Pipes.QueryableSource;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterQueryableSourcePipe(
            this IFluentRestBuilderCoreEntityFrameworkCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IQueryableSourcePipeFactory<,>), typeof(QueryableSourcePipeFactory<,>));
            return builder;
        }

        public static QueryableSourcePipe<TInput, TOutput> SelectQueryableSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<DbContext, TInput, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.GetRequiredService<IQueryableSourcePipeFactory<TInput, TOutput>>()
                .Create(selection, pipe);

        public static QueryableSourcePipe<TInput, TOutput> SelectQueryableSource<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<DbContext, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.SelectQueryableSource((queryableFactory, i) => selection(queryableFactory));
    }
}
