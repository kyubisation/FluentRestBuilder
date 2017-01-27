// <copyright file="DbContextScopedStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Storage
{
    using System;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DbContextScopedStorage<TContext> : IScopedStorage<DbContext>
        where TContext : DbContext
    {
        private Lazy<DbContext> lazyContext;

        public DbContextScopedStorage(IServiceProvider serviceProvider)
        {
            this.lazyContext = new Lazy<DbContext>(serviceProvider.GetService<TContext>);
        }

        public DbContext Value
        {
            get { return this.lazyContext.Value; }
            set { this.lazyContext = new Lazy<DbContext>(() => value); }
        }
    }
}
