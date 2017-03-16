// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using EntityFrameworkCore.Builder;
    using EntityFrameworkCore.Sources.QueryableSource;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderEntityFrameworkCoreConfiguration RegisterQueryableSource(
            this IFluentRestBuilderEntityFrameworkCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IQueryableSourceFactory<>), typeof(QueryableSourceFactory<>));
            return builder;
        }

        /// <summary>
        /// Gets an <see cref="IQueryable{TEntity}"/> from the <see cref="DbContext"/>;
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="controller">The MVC controller.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TEntity>> WithQueryable<TEntity>(
            this ControllerBase controller)
            where TEntity : class =>
            controller.HttpContext.RequestServices
                .GetService<IQueryableSourceFactory<TEntity>>()
                .Create(controller);
    }
}
