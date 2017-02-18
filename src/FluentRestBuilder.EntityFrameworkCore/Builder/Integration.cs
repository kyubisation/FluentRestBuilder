// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore.Builder;
    using Microsoft.EntityFrameworkCore;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreEntityFrameworkCore RegisterDbContext<TContext>(
            this IFluentRestBuilderCore builder)
            where TContext : DbContext =>
            new FluentRestBuilderCoreEntityFrameworkCore<TContext>(builder.Services);

        /// <summary>
        /// Register the Entity Framework Core related pipes.
        /// </summary>
        /// <typeparam name="TContext">The database context.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The given FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            new FluentRestBuilderCoreEntityFrameworkCore<TContext>(builder.Services)
                .RegisterDeletionPipe()
                .RegisterInputEntryAccessPipe()
                .RegisterInsertionPipe()
                .RegisterQueryableSourcePipe()
                .RegisterUpdatePipe()
                .RegisterQueryableSource()
                .RegisterEntitySource();
            return builder;
        }
    }
}
