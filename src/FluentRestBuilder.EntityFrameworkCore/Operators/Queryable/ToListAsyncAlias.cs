// <copyright file="ToListAsyncAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public static class ToListAsyncAlias
    {
        /// <summary>
        /// Asynchronously creates a <see cref="T:System.Collections.Generic.List`1" />
        /// from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it asynchronously.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<List<TSource>> ToListAsync<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable) =>
            observable.MapAsync(async s => await s.ToListAsync());
    }
}
