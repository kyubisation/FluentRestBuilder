// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Sources.EntitySource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderEntityFrameworkCoreConfiguration RegisterEntitySource(
            this IFluentRestBuilderEntityFrameworkCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IEntitySourceFactory<>), typeof(EntitySourceFactory<>));
            return builder;
        }

        /// <summary>
        /// Retrieves an entity from the database based on the predicate.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TEntity> WithEntity<TEntity>(
            this ControllerBase controller, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IEntitySourceFactory<TEntity>>()
                .Create(controller, predicate);

        /// <summary>
        /// Retrieves an entity from the database based on the primary key.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <param name="primaryKey">The primary key value or values.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TEntity> WithEntity<TEntity>(
            this ControllerBase controller, params object[] primaryKey)
            where TEntity : class
        {
            var factory = controller.HttpContext.RequestServices
                .GetService<IPrimaryKeyExpressionFactory<TEntity>>();
            return controller.WithEntity(factory.CreatePrimaryKeyFilterExpression(primaryKey));
        }
    }
}
