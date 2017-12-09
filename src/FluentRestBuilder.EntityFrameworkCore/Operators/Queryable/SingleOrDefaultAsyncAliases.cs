// <copyright file="SingleOrDefaultAsyncAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public static class SingleOrDefaultAsyncAliases
    {
        /// <summary>
        /// Emits  the only element of a sequence, or a default value
        /// if the sequence is empty; this method throws an exception
        /// if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> SingleOrDefaultAsync<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable) =>
            observable.MapAsync(async s => await s.SingleOrDefaultAsync());

        /// <summary>
        /// Emits the only element of a sequence that satisfies a specified
        /// condition or a default value if no such element exists; this
        /// method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> SingleOrDefaultAsync<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, bool>> predicate) =>
            observable.MapAsync(async s => await s.SingleOrDefaultAsync(predicate));
    }
}
