// <copyright file="DbContextScopedStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class DbContextScopedStorage<TContext> : ScopedStorage<DbContext>
        where TContext : DbContext
    {
        public DbContextScopedStorage(TContext context)
        {
            this.Value = context;
        }
    }
}
