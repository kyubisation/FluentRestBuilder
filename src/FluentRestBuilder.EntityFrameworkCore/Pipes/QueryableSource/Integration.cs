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

        /// <summary>
        /// Map to a <see cref="IQueryable{TOutput}"/> from the received <see cref="DbContext"/>.
        /// Use the <see cref="DbContext.Set{TEntity}"/> method to select the appropriate
        /// <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The entity type of the selected queryable.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="selection">The queryable selection.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static QueryableSourcePipe<TInput, TOutput> MapToQueryable<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<DbContext, TInput, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.GetRequiredService<IQueryableSourcePipeFactory<TInput, TOutput>>()
                .Create(selection, pipe);

        /// <summary>
        /// Use the <see cref="DbContext.Set{TEntity}"/> method to select the appropriate
        /// <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The entity type of the selected queryable.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="selection">The queryable selection.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static QueryableSourcePipe<TInput, TOutput> WithQueryable<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<DbContext, IQueryable<TOutput>> selection)
            where TOutput : class =>
            pipe.MapToQueryable((queryableFactory, i) => selection(queryableFactory));
    }
}
