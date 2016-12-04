// <copyright file="PropertyOrderByBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.MetaModel.OrderBy
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Metadata;
    using RestCollectionMutators.OrderBy;

    public class PropertyOrderByBuilder<TEntity> : IOrderByBuilder<TEntity>
    {
        private readonly ParameterExpression parameter;
        private readonly IProperty property;
        private readonly MemberExpression propertyExpression;

        public PropertyOrderByBuilder(IProperty property)
        {
            this.property = property;
            this.parameter = Expression.Parameter(this.property.DeclaringEntityType.ClrType);
            this.propertyExpression = Expression.Property(this.parameter, this.property.Name);
        }

        public OrderByDirection ResolveDirection(string orderBy)
        {
            if (orderBy.StartsWith("!") && this.CompareWithPropertyName(orderBy.Substring(1)))
            {
                return OrderByDirection.Descending;
            }

            return this.CompareWithPropertyName(orderBy)
                ? OrderByDirection.Ascending : OrderByDirection.None;
        }

        public Expression<Func<TEntity, object>> CreateOrderBy() =>
            Expression.Lambda<Func<TEntity, object>>(this.propertyExpression, this.parameter);

        private bool CompareWithPropertyName(string orderBy) =>
            string.Compare(this.property.Name, orderBy, StringComparison.OrdinalIgnoreCase) == 0;
    }
}
