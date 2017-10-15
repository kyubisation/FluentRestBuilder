// <copyright file="ThenIncludeAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    public static class ThenIncludeAlias
    {
        /// <summary>
        /// Specifies additional related data to be further included based on a
        /// related type that was just included.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TPreviousProperty">The type of the previous property.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="navigationPropertyPath">
        /// A lambda expression representing the navigation property to be
        /// included (<c>t =&gt; t.Property1</c>).
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IIncludableQueryable<TSource, TProperty>> ThenInclude<TSource, TPreviousProperty, TProperty>(
            this IProviderObservable<IIncludableQueryable<TSource, TPreviousProperty>> observable,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TSource : class =>
            observable.Map(s => s.ThenInclude(navigationPropertyPath));
    }
}
