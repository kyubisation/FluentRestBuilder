// <copyright file="DbContextContainer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;

    public class DbContextContainer<TContext> : IDbContextContainer
        where TContext : DbContext
    {
        public DbContextContainer(TContext context)
        {
            this.Context = context;
        }

        public DbContext Context { get; }
    }
}