// <copyright file="StringPropertyContainsFilterBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.MetaModel.Filters
{
    using System.Linq.Expressions;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class StringPropertyContainsFilterBuilder<TEntity> : StringPropertyFilterBuilder<TEntity>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly MethodInfo ContainsMethod = typeof(string)
            .GetRuntimeMethod(nameof(string.Contains), new[] { typeof(string) });

        public StringPropertyContainsFilterBuilder(IProperty property)
            : base(property)
        {
        }

        protected override Expression CreateFilterExpression(string filter) =>
            Expression.Call(
                this.PropertyExpression,
                ContainsMethod,
                Expression.Constant(filter));
    }
}
