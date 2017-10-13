// <copyright file="FluentRestBuilderConfigurationEntityFrameworkCoreIntegration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Linq;
    using System.Reflection;
    using Builder;
    using EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public static class FluentRestBuilderConfigurationEntityFrameworkCoreIntegration
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

        /// <summary>
        /// Configures filters and order by expressions for all non-complex properties
        /// of all entities in the given <see cref="DbContext"/>.
        /// <para>
        /// Allows <see cref="ApplyFilterByClientRequestOperator"/> and
        /// <see cref="ApplyOrderByClientRequestOperator"/> to be used without parameters.
        /// </para>
        /// </summary>
        /// <typeparam name="TContext">The <see cref="DbContext"/> type.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The given FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration ConfigureFiltersAndOrderByExpressionsForDbContextEntities<TContext>(
            this IFluentRestBuilderConfiguration builder)
            where TContext : DbContext
        {
            const string methodName = nameof(FluentRestBuilderConfigurationIntegration
                .ConfigureFiltersAndOrderByExpressionsForEntity);
            var properties = typeof(TContext).GetRuntimeProperties()
                .Where(p => p.GetGetMethod(true).IsPublic &&
                            p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
            var method = typeof(FluentRestBuilderConfigurationIntegration)
                .GetRuntimeMethod(methodName, new[] { typeof(IFluentRestBuilderConfiguration) });
            foreach (var property in properties)
            {
                var entityType = property.PropertyType.GenericTypeArguments
                    .Single();
                method.MakeGenericMethod(entityType)
                    .Invoke(null, new object[] { builder });
            }

            return builder;
        }
    }
}
