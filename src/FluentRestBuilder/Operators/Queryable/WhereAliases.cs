// <copyright file="WhereAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class WhereAliases
    {
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IQueryable}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> Where<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, bool>> predicate) =>
            observable.Map(s => s.Where(predicate));

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// Each element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="predicate">
        /// A function to test each element for a condition; the second
        /// parameter of the function represents the index of the element in the source sequence.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{IQueryable}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> Where<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, int, bool>> predicate) =>
            observable.Map(s => s.Where(predicate));
    }
}
