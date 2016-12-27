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
    using RestCollectionMutators.Filter;
    using RestCollectionMutators.Pagination;
    using RestCollectionMutators.Search;

    public static partial class Integration
    {
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

        public static OutputPipe<IQueryable<TInput>> ApplyFilter<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            IRestCollectionFilter<TInput> filter) =>
            pipe.MapQueryable(filter.Apply);

        public static OutputPipe<IQueryable<TInput>> ApplySearch<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            IRestCollectionSearch<TInput> search) =>
            pipe.MapQueryable(search.Apply);

        public static OutputPipe<IQueryable<TInput>> ApplyPagination<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            IRestCollectionPagination<TInput> pagination) =>
            pipe.MapQueryable(pagination.Apply);

        public static OutputPipe<IQueryable<TInput>> ApplyPagination<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            IRestCollectionPagination<TInput> pagination,
            PaginationOptions options)
        {
            pagination.Options = options;
            return pipe.MapQueryable(pagination.Apply);
        }
    }
}
