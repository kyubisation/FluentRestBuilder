// <copyright file="WithEntityEntryAsyncAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public static class WithEntityEntryAsyncAlias
    {
        /// <summary>
        /// Perform an async action with the <see cref="EntityEntry{TEntity}"/>
        /// of the received value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="action">The action to be performed.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> WithEntityEntryAsync<TSource>(
            this IProviderObservable<TSource> observable, Func<EntityEntry<TSource>, Task> action)
            where TSource : class =>
            observable.WithDbContextAsync(async (source, context) =>
            {
                var entityEntry = context.Entry(source);
                await action(entityEntry);
            });
    }
}
