// <copyright file="ToListAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ToListAlias
    {
        /// <summary>
        /// Creates and emits a <see cref="T:System.Collections.Generic.List`1"></see>
        /// from an <see cref="T:System.Collections.Generic.IEnumerable`1"></see>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TFrom}"/>.</returns>
        public static IProviderObservable<List<TSource>> ToList<TSource>(
            this IProviderObservable<IEnumerable<TSource>> observable) =>
            observable.Map(s => s.ToList());
    }
}
