// <copyright file="WithEntityEntryAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public static class WithEntityEntryAlias
    {
        /// <summary>
        /// Perform an action with the <see cref="EntityEntry{TEntity}"/> of the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithEntityEntry<TSource>(
            this IProviderObservable<TSource> observable, Action<EntityEntry<TSource>> action)
            where TSource : class =>
            observable.WithDbContext((source, context) =>
            {
                var entityEntry = context.Entry(source);
                action(entityEntry);
            });
    }
}
