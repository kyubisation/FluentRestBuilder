// <copyright file="SelectableEntitySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Sources.SelectableEntity
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Common;
    using Core;
    using Core.Sources.Common;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class SelectableEntitySource<TEntity> :
        EntitySource<TEntity, SelectableEntitySource<TEntity>>,
        IOutputPipe<TEntity>
        where TEntity : class
    {
        private readonly IServiceProvider serviceProvider;
        private IInputPipe<TEntity> child;
        private TEntity storedEntity;

        public SelectableEntitySource(
            Expression<Func<TEntity, bool>> selectionFilter,
            IQueryable<TEntity> queryable,
            IServiceProvider serviceProvider)
            : base(queryable)
        {
            this.serviceProvider = serviceProvider;
            this.Queryable = this.Queryable.Where(selectionFilter);
        }

        public static SelectableEntitySource<TEntity> Resolve(
            Expression<Func<TEntity, bool>> selectionFilter,
            IServiceProvider serviceProvider) =>
            new SelectableEntitySource<TEntity>(
                selectionFilter,
                serviceProvider.GetService<IQueryableFactory<TEntity>>().Queryable,
                serviceProvider);

        object IServiceProvider.GetService(Type serviceType)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        object IItemProvider.GetItem(Type itemType)
        {
            if (itemType == typeof(TEntity))
            {
                return this.storedEntity;
            }

            if (itemType == typeof(IQueryable<TEntity>))
            {
                return this.Queryable;
            }

            return null;
        }

        TPipe IOutputPipe<TEntity>.Attach<TPipe>(TPipe pipe)
        {
            this.child = pipe;
            return pipe;
        }

        async Task<IActionResult> IPipe.Execute()
        {
            NoPipeAttachedException.Check(this.child);
            this.storedEntity = await this.Queryable.SingleOrDefaultAsync();
            return await this.child.Execute(this.storedEntity);
        }
    }
}
