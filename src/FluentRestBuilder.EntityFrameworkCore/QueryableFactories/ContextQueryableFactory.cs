// <copyright file="ContextQueryableFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.QueryableFactories
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class ContextQueryableFactory<TContext> : IQueryableFactory
        where TContext : DbContext
    {
        private readonly TContext context;

        public ContextQueryableFactory(TContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Resolve<TEntity>()
            where TEntity : class =>
            this.context.Set<TEntity>();
    }
}