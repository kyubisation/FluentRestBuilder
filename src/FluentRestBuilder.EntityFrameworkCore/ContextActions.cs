// <copyright file="ContextActions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class ContextActions<TContext> : IContextActions
        where TContext : DbContext
    {
        private readonly TContext context;

        public ContextActions(TContext context)
        {
            this.context = context;
        }

        public async Task<int> RemoveAndSave<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.context.Remove(entity);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> AddAndSave<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.context.Add(entity);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateAndSave<TEntity>(TEntity entity)
            where TEntity : class
        {
            return await this.context.SaveChangesAsync();
        }

        public async Task Reload<TEntity>(TEntity entity)
            where TEntity : class
        {
            await this.context.Entry(entity).ReloadAsync();
        }
    }
}