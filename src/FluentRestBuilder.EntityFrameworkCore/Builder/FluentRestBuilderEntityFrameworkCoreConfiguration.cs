// <copyright file="FluentRestBuilderEntityFrameworkCoreConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Builder
{
    using FluentRestBuilder.Storage;
    using MetaModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public class FluentRestBuilderEntityFrameworkCoreConfiguration<TContext> : IFluentRestBuilderEntityFrameworkCoreConfiguration
        where TContext : DbContext
    {
        public FluentRestBuilderEntityFrameworkCoreConfiguration(IServiceCollection services)
        {
            this.Services = services;
            this.Services
                .TryAddScoped<IScopedStorage<DbContext>, DbContextScopedStorage<TContext>>();
            this.Services.TryAddSingleton<IModelContainer, ModelContainer>();
            this.Services.TryAddSingleton(
                typeof(IPredicateBuilder<>), typeof(PredicateBuilder<>));
            this.Services.TryAddSingleton(
                typeof(IPrimaryKeyExpressionFactory<>), typeof(PrimaryKeyExpressionFactory<>));
        }

        public IServiceCollection Services { get; }
    }
}