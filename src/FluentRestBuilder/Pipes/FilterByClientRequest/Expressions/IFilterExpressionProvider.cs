// <copyright file="IFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Linq.Expressions;

    public interface IFilterExpressionProvider<TEntity>
    {
        Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter);
    }
}
