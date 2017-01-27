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

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> AsNoTracking<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe)
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
    }
}
