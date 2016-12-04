// <copyright file="StringPropertyFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.MetaModel.Filters
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Metadata;

    public abstract class StringPropertyFilterBuilder<TEntity> : PropertyFilterBuilder<TEntity>
    {
        protected StringPropertyFilterBuilder(IProperty property)
            : base(property)
        {
        }

        protected override Expression CreateFilterExpression(object filter)
        {
            var stringFilter = filter as string;
            return stringFilter != null
                ? this.CreateFilterExpression(stringFilter)
                : Expression.Constant(false);
        }

        protected abstract Expression CreateFilterExpression(string filter);
    }
}
