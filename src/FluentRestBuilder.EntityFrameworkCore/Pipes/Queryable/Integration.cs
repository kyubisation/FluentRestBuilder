// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Microsoft.Extensions.DependencyInjection;
    using RestCollectionMutators.Filter;
    using RestCollectionMutators.OrderBy;
    using RestCollectionMutators.Pagination;
    using RestCollectionMutators.Search;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> AsNoTracking<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.MapQueryable(q => q.AsNoTracking());

        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> Include<TInput, TProperty>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Expression<Func<TInput, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.MapQueryable(q => q.Include(navigationPropertyPath));

        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> ThenInclude<TInput, TPreviousProperty, TProperty>(
            this IOutputPipe<IIncludableQueryable<TInput, TPreviousProperty>> pipe,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenInclude(navigationPropertyPath));

        public static OutputPipe<IQueryable<TInput>> ApplyFilter<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe)
        {
            var filter = pipe.GetService<IRestCollectionFilter<TInput>>();
            return pipe.MapQueryable(filter.Apply);
        }

        public static OutputPipe<IQueryable<TInput>> ApplySearch<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe)
        {
            var search = pipe.GetService<IRestCollectionSearch<TInput>>();
            return pipe.MapQueryable(search.Apply);
        }

        public static OutputPipe<IQueryable<TInput>> ApplyOrderBy<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe)
        {
            var orderBy = pipe.GetService<IRestCollectionOrderBy<TInput>>();
            return pipe.MapQueryable(orderBy.Apply);
        }

        public static OutputPipe<IQueryable<TInput>> ApplyPagination<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe)
        {
            var pagination = pipe.GetService<IRestCollectionPagination<TInput>>();
            return pipe.MapQueryable(pagination.Apply);
        }

        public static OutputPipe<IQueryable<TInput>> ApplyPagination<TInput>(
            this IOutputPipe<IOrderedQueryable<TInput>> pipe,
            PaginationOptions options)
        {
            var pagination = pipe.GetService<IRestCollectionPagination<TInput>>();
            pagination.Options = options;
            return pipe.MapQueryable(pagination.Apply);
        }
    }
}
