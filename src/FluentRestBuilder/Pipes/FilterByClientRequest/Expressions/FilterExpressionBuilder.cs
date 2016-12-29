// <copyright file="FilterExpressionBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public class FilterExpressionBuilder<TEntity> : IFilterExpressionBuilder<TEntity>
    {
        private readonly IServiceProvider provider;

        private readonly IDictionary<string, IFilterExpressionProvider<TEntity>> filters =
            new Dictionary<string, IFilterExpressionProvider<TEntity>>();

        public FilterExpressionBuilder(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IFilterExpressionBuilder<TEntity> AddFilter(
            string property, FilterExpressionBuilderCallback<TEntity, string> builder)
        {
            var providerBuilder = this.CreateProviderBuilder();
            this.filters.Add(
                property, new StringFilterExpressionProvider<TEntity>(
                    f => builder(f, providerBuilder)));
            return this;
        }

        public IFilterExpressionBuilder<TEntity> AddTypedFilter<TFilter>(
            string property, FilterExpressionBuilderCallback<TEntity, TFilter> builder) =>
            this.AddTypedFilter(property, ConvertToFilterType<TFilter>, builder);

        public IFilterExpressionBuilder<TEntity> AddTypedFilter<TFilter>(
            string property,
            Func<string, TFilter> conversion,
            FilterExpressionBuilderCallback<TEntity, TFilter> builder)
        {
            var providerBuilder = this.CreateProviderBuilder();
            this.filters.Add(
                property, new GenericFilterExpressionProvider<TEntity,TFilter>(
                    f => builder(f, providerBuilder), conversion));
            return this;
        }

        public IDictionary<string, IFilterExpressionProvider<TEntity>> Build() => this.filters;

        private static TFilter ConvertToFilterType<TFilter>(string filter) =>
            (TFilter)Convert.ChangeType(filter, typeof(TFilter));

        private IFilterExpressionProviderBuilder<TEntity> CreateProviderBuilder() =>
            this.provider.GetService<IFilterExpressionProviderBuilder<TEntity>>();
    }
}