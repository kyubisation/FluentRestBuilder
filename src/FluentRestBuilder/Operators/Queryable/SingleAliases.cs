// <copyright file="SingleAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class SingleAliases
    {
        /// <summary>
        /// Emits the only element of a sequence, and throws an exception
        /// if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> Single<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable) =>
            observable.Map(s => s.Single());

        /// <summary>
        /// Emits the only element of a sequence that satisfies a specified
        /// condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<TSource> Single<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, bool>> predicate) =>
            observable.Map(s => s.Single(predicate));
    }
}
