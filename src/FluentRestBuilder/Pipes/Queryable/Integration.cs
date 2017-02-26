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
    using Pipes.Queryable;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterQueryablePipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IQueryablePipeFactory<,>), typeof(QueryablePipeFactory<,>));
            return builder;
        }

        /// <summary>
        /// Use any kind of extension method on the received <see cref="IQueryable"/>
        /// that results in any kind of <see cref="IQueryable"/>.
        /// </summary>
        /// <typeparam name="TInputQueryable">The input <see cref="Queryable"/>.</typeparam>
        /// <typeparam name="TOutputQueryable">The output <see cref="Queryable"/>.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutputQueryable> MapQueryable<TInputQueryable, TOutputQueryable>(
            this IOutputPipe<TInputQueryable> pipe, Func<TInputQueryable, TOutputQueryable> mapping)
            where TInputQueryable : class, IQueryable
            where TOutputQueryable : class, IQueryable
        {
            var factory = pipe
                .GetService<IQueryablePipeFactory<TInputQueryable, TOutputQueryable>>();
            Check.IsPipeRegistered(factory, typeof(QueryablePipe<,>));
            return factory.Create(mapping, pipe);
        }

        /// <summary>
        /// Filter the received <see cref="IQueryable{TInput}"/> with the given predicate.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> Where<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.MapQueryable(q => q.Where(predicate));

        /// <summary>
        /// Order by the given expression.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TKey">The key type of the field/property to order by.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keySelector">The key selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IOrderedQueryable<TInput>> OrderBy<TInput, TKey>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.OrderBy(keySelector));

        /// <summary>
        /// Order by descending with the given expression.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TKey">The key type of the field/property to order by.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keySelector">The key selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IOrderedQueryable<TInput>> OrderByDescending<TInput, TKey>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.OrderByDescending(keySelector));

        /// <summary>
        /// Then order by the given expression.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TKey">The key type of the field/property to order by.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keySelector">The key selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IOrderedQueryable<TInput>> ThenBy<TInput, TKey>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenBy(keySelector));

        /// <summary>
        /// Then order by descending with the given expression.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TKey">The key type of the field/property to order by.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="keySelector">The key selection expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IOrderedQueryable<TInput>> ThenByDescending<TInput, TKey>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenByDescending(keySelector));
    }
}
