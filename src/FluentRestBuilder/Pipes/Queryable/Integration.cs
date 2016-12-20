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
    using Pipes.Queryable;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> Where<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IQueryable<TInput>, IQueryable<TInput>>>()
                .Resolve(q => q.Where(predicate), pipe);

        public static OutputPipe<IOrderedQueryable<TInput>> OrderBy<TInput, TKey>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IQueryable<TInput>, IOrderedQueryable<TInput>>>()
                .Resolve(q => q.OrderBy(keySelector), pipe);

        public static OutputPipe<IOrderedQueryable<TInput>> ThenBy<TInput, TKey>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            Expression<Func<TInput, TKey>> keySelector)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IOrderedQueryable<TInput>, IOrderedQueryable<TInput>>>()
                .Resolve(q => q.ThenBy(keySelector), pipe);
    }
}
