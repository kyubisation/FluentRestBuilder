// <copyright file="FirstOrDefaultAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class FirstOrDefaultAliases
    {
        /// <summary>
        /// Emits the first element of a sequence, or a default
        /// value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> FirstOrDefault<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable) =>
            observable.Map(s => s.FirstOrDefault());

        /// <summary>
        /// Emits the first element of a sequence that satisfies a
        /// specified condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> FirstOrDefault<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, bool>> predicate) =>
            observable.Map(s => s.FirstOrDefault(predicate));
    }
}
