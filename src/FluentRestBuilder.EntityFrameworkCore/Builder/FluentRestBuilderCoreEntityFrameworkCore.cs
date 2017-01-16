// <copyright file="FluentRestBuilderCoreEntityFrameworkCore.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Builder
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class FluentRestBuilderCoreEntityFrameworkCore<TContext> : IFluentRestBuilderCoreEntityFrameworkCore
        where TContext : DbContext
    {
        public FluentRestBuilderCoreEntityFrameworkCore(IServiceCollection services)
        {
            this.Services = services;
            this.Services.TryAddScoped<IDbContextContainer, DbContextContainer<TContext>>();
        }

        public IServiceCollection Services { get; }
    }
}