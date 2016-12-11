// <copyright file="SelectableEntitySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.SelectableEntity
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Common;
    using EntityFrameworkCore.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class SelectableEntitySource<TEntity> :
        EntitySource<TEntity, SelectableEntitySource<TEntity>>,
        IOutputPipe<TEntity>
        where TEntity : class
    {
        private readonly IScopedStorage<TEntity> entityStorage;
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<TEntity> child;

        public SelectableEntitySource(
            Expression<Func<TEntity, bool>> selectionFilter,
            IQueryable<TEntity> queryable,
            IScopedStorage<TEntity> entityStorage,
            IServiceProvider serviceProvider)
            : base(queryable)
        {
            this.entityStorage = entityStorage;
            this.serviceProvider = serviceProvider;
            this.Queryable = this.Queryable.Where(selectionFilter);
        }

        public static SelectableEntitySource<TEntity> Resolve(
            Expression<Func<TEntity, bool>> selectionFilter,
            IServiceProvider serviceProvider) =>
            new SelectableEntitySource<TEntity>(
                selectionFilter,
                serviceProvider.GetService<IQueryableFactory<TEntity>>().Queryable,
                serviceProvider.GetService<IScopedStorage<TEntity>>(),
                serviceProvider);

        object IServiceProvider.GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        TPipe IOutputPipe<TEntity>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        async Task<IActionResult> IPipe.Execute()
        {
            NoPipeAttachedException.Check(this.child);
            var entity = await this.Queryable.SingleOrDefaultAsync();
            this.entityStorage.Value = entity;
            return await this.child.Execute(entity);
        }
    }
}
