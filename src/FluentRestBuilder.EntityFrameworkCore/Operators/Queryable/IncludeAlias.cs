// <copyright file="IncludeAlias.cs" company="Kyubisation">
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

    public static class IncludeAlias
    {
        /// <summary>
        /// Specifies related entities to include in the query results. The navigation property
        /// to be included is specified starting with the type of entity being queried
        /// (<typeparamref name="TSource" />). If you wish to include additional types based on the
        /// navigation properties of the type being included, then chain a call to
        /// <see cref="M:Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ThenInclude``3(Microsoft.EntityFrameworkCore.Query.IIncludableQueryable{``0,System.Collections.Generic.IEnumerable{``1}},System.Linq.Expressions.Expression{System.Func{``1,``2}})" />
        /// after this call.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="navigationPropertyPath">
        /// A lambda expression representing the navigation property to be
        /// included (<c>t =&gt; t.Property1</c>).
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IIncludableQueryable<TSource, TProperty>> Include<TSource, TProperty>(
            this IProviderObservable<IQueryable<TSource>> observable,
            Expression<Func<TSource, TProperty>> navigationPropertyPath)
            where TSource : class =>
            observable.Map(s => s.Include(navigationPropertyPath));
    }
}
