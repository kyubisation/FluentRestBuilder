// <copyright file="PropertyFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel.Filters
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Metadata;

    public abstract class PropertyFilterBuilder<TEntity> : IFilterBuilder<TEntity>
    {
        private readonly ParameterExpression parameter;

        protected PropertyFilterBuilder(IProperty property)
        {
            this.Property = property;
            this.parameter = Expression.Parameter(this.Property.DeclaringEntityType.ClrType);
            this.PropertyExpression = Expression.Property(this.parameter, this.Property.Name);
        }

        protected IProperty Property { get; }

        protected MemberExpression PropertyExpression { get; }

        public Expression<Func<TEntity, bool>> CreateFilter(object filter) =>
            Expression.Lambda<Func<TEntity, bool>>(
                this.CreateFilterExpression(filter), this.parameter);

        protected abstract Expression CreateFilterExpression(object filter);
    }
}
