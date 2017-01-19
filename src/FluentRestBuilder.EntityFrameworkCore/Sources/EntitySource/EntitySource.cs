// <copyright file="EntitySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.EntitySource
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using FluentRestBuilder.Sources;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;

    public class EntitySource<TEntity> : SourceBase<TEntity>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> predicate;
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntitySource(
            Expression<Func<TEntity, bool>> predicate,
            IScopedStorage<DbContext> contextStorage,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.predicate = predicate;
            this.contextStorage = contextStorage;
        }

        protected override async Task<TEntity> GetOutput() =>
            await this.contextStorage.Value
                .Set<TEntity>()
                .SingleOrDefaultAsync(this.predicate);
    }
}
