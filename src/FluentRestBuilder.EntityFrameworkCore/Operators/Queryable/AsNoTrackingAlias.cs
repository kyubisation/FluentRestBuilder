// <copyright file="AsNoTrackingAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public static class AsNoTrackingAlias
    {
        /// <summary>
        ///     <para>
        ///         Returns a new query where the change tracker will not track any of the entities
        ///         that are returned. If the entity instances are modified, this will not be
        ///         detected by the change tracker and
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" /> will not
        ///         persist those changes to the database.
        ///     </para>
        ///     <para>
        ///         Disabling change tracking is useful for read-only scenarios because it avoids
        ///         the overhead of setting up change tracking for each entity instance. You should
        ///         not disable change tracking if you want to manipulate entity instances and
        ///         persist those changes to the database using
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChanges" />.
        ///     </para>
        ///     <para>
        ///         Identity resolution will still be performed to ensure that all occurrences of
        ///         an entity with a given key in the result set are represented by the same entity
        ///         instance.
        ///     </para>
        ///     <para>
        ///         The default tracking behavior for queries can be controlled by
        ///         <see cref="P:Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.QueryTrackingBehavior" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> AsNoTracking<TSource>(
            this IProviderObservable<IQueryable<TSource>> observable)
            where TSource : class =>
            observable.Map(s => s.AsNoTracking());
    }
}
