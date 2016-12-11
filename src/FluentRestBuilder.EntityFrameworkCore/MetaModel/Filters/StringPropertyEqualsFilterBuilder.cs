// <copyright file="StringPropertyEqualsFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel.Filters
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class StringPropertyEqualsFilterBuilder<TEntity> : StringPropertyFilterBuilder<TEntity>
    {
        public StringPropertyEqualsFilterBuilder(IProperty property)
            : base(property)
        {
        }

        protected override Expression CreateFilterExpression(string filter) =>
            Expression.Equal(this.PropertyExpression, Expression.Constant(filter));
    }
}
