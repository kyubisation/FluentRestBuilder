// <copyright file="IFilterExpressionBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;

    public delegate IFilterExpressionProviderBuilder<TEntity>
        FilterExpressionBuilderCallback<TEntity, in TFilter>(
        TFilter filter, IFilterExpressionProviderBuilder<TEntity> builder);

    public interface IFilterExpressionBuilder<TEntity>
    {
        IFilterExpressionBuilder<TEntity> AddFilter(
            string property, FilterExpressionBuilderCallback<TEntity, string> builder);

        IFilterExpressionBuilder<TEntity> AddTypedFilter<TFilter>(
            string property, FilterExpressionBuilderCallback<TEntity, TFilter> builder);

        IFilterExpressionBuilder<TEntity> AddTypedFilter<TFilter>(
            string property,
            Func<string, TFilter> conversion,
            FilterExpressionBuilderCallback<TEntity, TFilter> builder);

        IDictionary<string, IFilterExpressionProvider<TEntity>> Build();
    }
}
