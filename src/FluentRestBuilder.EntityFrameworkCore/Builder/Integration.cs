// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public static partial class Integration
    {
        /// <summary>
        /// Register the Entity Framework Core related pipes.
        /// </summary>
        /// <typeparam name="TContext">The database context.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The given FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddEntityFrameworkCoreIntegration<TContext>(
            this IFluentRestBuilderConfiguration builder)
            where TContext : DbContext
        {
            builder.Services
                .TryAddScoped<IScopedStorage<DbContext>, DbContextScopedStorage<TContext>>();
            return builder;
        }
    }
}
