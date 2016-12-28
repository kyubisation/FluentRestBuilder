// <copyright file="StringFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class StringFilterExpressionProvider<TEntity> : FilterExpressionProvider<TEntity, string>
    {
        public StringFilterExpressionProvider(
            IDictionary<FilterType, Func<string, Expression<Func<TEntity, bool>>>> filterDictionary)
            : base(filterDictionary)
        {
        }

        public override Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter) =>
            this.ResolveForType(type, filter);
    }
}