// <copyright file="EntitySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Sources.Common
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public abstract class EntitySource<TEntity, TDerived>
        where TEntity : class
        where TDerived : class
    {
        protected EntitySource(IQueryable<TEntity> queryable)
        {
            this.Queryable = queryable;
        }

        protected IQueryable<TEntity> Queryable { get; set; }

        public TDerived AsNoTracking()
        {
            this.Queryable = this.Queryable.AsNoTracking();
            return this as TDerived;
        }

        public TDerived Include<TProperty>(
            Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            this.Queryable = this.Queryable.Include(navigationPropertyPath);
            return this as TDerived;
        }

        public TDerived Include<TProperty, TNestedProperty>(
            Expression<Func<TEntity, TProperty>> navigationPropertyPath,
            Expression<Func<TProperty, TNestedProperty>> nestedNavigationPropertyPath)
        {
            this.Queryable = this.Queryable
                .Include(navigationPropertyPath)
                .ThenInclude(nestedNavigationPropertyPath);
            return this as TDerived;
        }
    }
}
