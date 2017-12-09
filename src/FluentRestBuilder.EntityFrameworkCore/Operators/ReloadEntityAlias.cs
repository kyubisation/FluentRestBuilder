// <copyright file="ReloadEntityAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    public static class ReloadEntityAlias
    {
        /// <summary>
        ///     <para>
        ///         Reloads the entity from the database overwriting any property
        ///         values with values from the database.
        ///     </para>
        ///     <para>
        ///         The entity will be in the
        ///         <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Unchanged" /> state
        ///         after calling this method, unless the entity does not exist in the database,
        ///         in which case the entity will be
        ///         <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Detached" />. Finally,
        ///         calling Reload on an <see cref="F:Microsoft.EntityFrameworkCore.EntityState.Added" />
        ///         entity that does not exist in the database is a no-op. Note, however, that
        ///         an Added entity may not yet have had its permanent key value created.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> ReloadEntity<TSource>(
            this IProviderObservable<TSource> observable)
            where TSource : class =>
            observable.WithEntityEntryAsync(async s => await s.ReloadAsync());
    }
}
