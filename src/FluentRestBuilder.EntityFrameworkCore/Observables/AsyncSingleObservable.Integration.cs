// <copyright file="AsyncSingleObservable.Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public static partial class Integration
    {
        /// <summary>
        /// Create an observable which emits either the entity found via the given predicate
        /// or emits null if nothing was found.
        /// </summary>
        /// <typeparam name="TSource">The entity type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateEntitySingle<TSource>(
            this ControllerBase controller, Expression<Func<TSource, bool>> predicate)
            where TSource : class
        {
            Check.IsNull(controller, nameof(controller));
            Check.IsNull(predicate, nameof(predicate));
            return controller.CreateQueryableSingle<TSource>()
                .SingleOrDefault(predicate);
        }

        /// <summary>
        /// Create an observable which emits either the entity found via the given keys
        /// or emits null if nothing was found.
        /// </summary>
        /// <typeparam name="TSource">The entity type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="keyValues">
        /// The values of the primary key for the entity to be found.
        /// </param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<TSource> CreateEntitySingle<TSource>(
            this ControllerBase controller, params object[] keyValues)
            where TSource : class
        {
            Check.IsNull(controller, nameof(controller));
            Check.IsNull(keyValues, nameof(keyValues));
            var serviceProvider = controller.HttpContext.RequestServices;
            return Observable.AsyncSingle(
                () => ResolveQueryable<TSource>(serviceProvider)
                    .FindAsync(keyValues),
                serviceProvider);
        }

        /// <summary>
        /// Create an observable which emits an <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The queryable type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <returns>An instance of <see cref="IProviderObservable{T}"/>.</returns>
        public static IProviderObservable<IQueryable<TSource>> CreateQueryableSingle<TSource>(
            this ControllerBase controller)
            where TSource : class
        {
            Check.IsNull(controller, nameof(controller));
            var serviceProvider = controller.HttpContext.RequestServices;
            return Observable.AsyncSingle(
                () => ResolveQueryable<TSource>(serviceProvider), serviceProvider);
        }

        private static DbSet<TSource> ResolveQueryable<TSource>(IServiceProvider serviceProvider)
            where TSource : class
        {
            var contextStorage = serviceProvider.GetService<IScopedStorage<DbContext>>();
            return contextStorage.Value.Set<TSource>();
        }
    }
}
