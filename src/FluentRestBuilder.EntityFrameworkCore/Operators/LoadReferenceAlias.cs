// <copyright file="LoadReferenceAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;

    public static class LoadReferenceAlias
    {
        /// <summary>
        /// Load a single reference from the database.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <typeparam name="TProperty">The type of the reference.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="propertyExpression">The property selection expression.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> LoadReference<TSource, TProperty>(
            this IProviderObservable<TSource> observable,
            Expression<Func<TSource, TProperty>> propertyExpression)
            where TSource : class
            where TProperty : class =>
            observable.WithEntityEntryAsync(
                async s => await s.Reference(propertyExpression).LoadAsync());
    }
}
