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
        /// <summary>
        /// Marks the <see cref="IQueryable{TInput}"/> to not track the resulting entities.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> AsNoTracking<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe)
            where TInput : class =>
            pipe.MapQueryable(q => q.AsNoTracking());

        /// <summary>
        /// Specifies related entities to include in the query result.
        /// Use <see cref="ThenInclude{TInput,TPreviousProperty,TProperty}"/> to
        /// include entities from more than one navigation.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TProperty">The navigation property type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="navigationPropertyPath">
        /// The navigation property selection expression.
        /// </param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> Include<TInput, TProperty>(
            this IOutputPipe<IQueryable<TInput>> pipe,
            Expression<Func<TInput, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.MapQueryable(q => q.Include(navigationPropertyPath));

        /// <summary>
        /// Specifies additional related data to be further included
        /// based on a related type that was just included.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TPreviousProperty">The previous navigation property type.</typeparam>
        /// <typeparam name="TProperty">The next navigation property type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="navigationPropertyPath">
        /// The navigation property selection expression.
        /// </param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IIncludableQueryable<TInput, TProperty>> ThenInclude<TInput, TPreviousProperty, TProperty>(
            this IOutputPipe<IIncludableQueryable<TInput, TPreviousProperty>> pipe,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TInput : class =>
            pipe.MapQueryable(q => q.ThenInclude(navigationPropertyPath));
    }
}
