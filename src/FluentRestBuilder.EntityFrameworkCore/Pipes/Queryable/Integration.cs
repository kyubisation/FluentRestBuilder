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
    using Pipes.Queryable;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> AsNoTracking<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IQueryable<TInput>, IQueryable<TInput>>>()
                .Resolve(q => q.AsNoTracking(), pipe);

        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> Include<TInput, TProperty>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Expression<Func<TInput, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IQueryable<TInput>, IIncludableQueryable<TInput, TProperty>>>()
                .Resolve(q => q.Include(navigationPropertyPath), pipe);

        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> ThenInclude<TInput, TPreviousProperty, TProperty>(
            this IOutputPipe<IIncludableQueryable<TInput, TPreviousProperty>> pipe,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.GetService<IQueryablePipeFactory<IIncludableQueryable<TInput, TPreviousProperty>, IIncludableQueryable<TInput, TProperty>>>()
                .Resolve(q => q.ThenInclude(navigationPropertyPath), pipe);
    }
}
