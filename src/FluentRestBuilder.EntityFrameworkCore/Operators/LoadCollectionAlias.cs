// <copyright file="LoadCollectionAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class LoadCollectionAlias
    {
        /// <summary>
        /// Load a reference collection from the database.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TProperty">The type of the reference collection.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="propertyExpression">The property selection expression.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> LoadCollection<TSource, TProperty>(
            this IProviderObservable<TSource> observable,
            Expression<Func<TSource, IEnumerable<TProperty>>> propertyExpression)
            where TSource : class
            where TProperty : class =>
            observable.WithEntityEntryAsync(
                async s => await s.Collection(propertyExpression).LoadAsync());
    }
}
