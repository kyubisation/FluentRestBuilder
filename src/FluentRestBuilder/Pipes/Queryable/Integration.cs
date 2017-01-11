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

        public static OutputPipe<TOutputQueryable> MapQueryable<TInputQueryable, TOutputQueryable>(
            this IOutputPipe<TInputQueryable> pipe, Func<TInputQueryable, TOutputQueryable> mapping)
            where TInputQueryable : class, IQueryable
            where TOutputQueryable : class, IQueryable =>
            pipe.GetService<IQueryablePipeFactory<TInputQueryable, TOutputQueryable>>()
                .Resolve(mapping, pipe);

        public static OutputPipe<IQueryable<TInput>> Where<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.MapQueryable(q => q.Where(predicate));

        public static OutputPipe<IOrderedQueryable<TInput>> OrderBy<TInput, TKey>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.OrderBy(keySelector));

        public static OutputPipe<IOrderedQueryable<TInput>> OrderByDescending<TInput, TKey>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.OrderByDescending(keySelector));

        public static OutputPipe<IOrderedQueryable<TInput>> ThenBy<TInput, TKey>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenBy(keySelector));

        public static OutputPipe<IOrderedQueryable<TInput>> ThenByDescending<TInput, TKey>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenByDescending(keySelector));
    }
}
