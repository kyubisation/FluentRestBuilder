// <copyright file="ValueTypePropertyEqualsFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel.Filters
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class ValueTypePropertyEqualsFilterBuilder<TEntity> : PropertyFilterBuilder<TEntity>
    {
        public ValueTypePropertyEqualsFilterBuilder(IProperty property)
            : base(property)
        {
        }

        protected override Expression CreateFilterExpression(object filter)
        {
            try
            {
                var value = Convert.ChangeType(filter, this.Property.ClrType);
                return Expression.Equal(this.PropertyExpression, Expression.Constant(value));
            }
            catch (Exception)
            {
                return Expression.Constant(false);
            }
        }
    }
}
