// <copyright file="MapToQueryableAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public static class MapToQueryableAlias
    {
        /// <summary>
        /// Map to a <see cref="IQueryable{T}"/> from the received <see cref="DbContext"/>.
        /// Use the <see cref="DbContext.Set{TEntity}"/> method to select the appropriate
        /// <see cref="IQueryable{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TTarget">The type of the resulting queryable.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="mapping">The mapping function</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TTarget>> MapToQueryable<TSource, TTarget>(
            this IProviderObservable<TSource> observable,
            Func<DbContext, IQueryable<TTarget>> mapping)
            where TSource : class =>
            observable.MapToQueryable((s, c) => mapping(c));
    }
}
