// <copyright file="ThenByDescendingAliases.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ThenByDescendingAliases
    {
        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence
        /// in descending order, according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An instance of <see cref="IProviderObservable{IOrderedQueryable}"/>.</returns>
        public static IProviderObservable<IOrderedQueryable<TSource>> ThenByDescending<TSource, TKey>(
            this IProviderObservable<IOrderedQueryable<TSource>> observable,
            Expression<Func<TSource, TKey>> keySelector) =>
            observable.Map(s => s.ThenByDescending(keySelector));

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence
        /// in descending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">
        /// An <see cref="T:System.Collections.Generic.IComparer`1"></see> to compare keys.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{IOrderedQueryable}"/>.</returns>
        public static IProviderObservable<IOrderedQueryable<TSource>> ThenByDescending<TSource, TKey>(
            this IProviderObservable<IOrderedQueryable<TSource>> observable,
            Expression<Func<TSource, TKey>> keySelector,
            IComparer<TKey> comparer) =>
            observable.Map(s => s.ThenByDescending(keySelector, comparer));
    }
}
