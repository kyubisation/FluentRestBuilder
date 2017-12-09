// <copyright file="IFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Linq.Expressions;
    using Interpreters.Requests;

    public interface IFilterExpressionProvider<TEntity>
    {
        Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter);
    }
}
