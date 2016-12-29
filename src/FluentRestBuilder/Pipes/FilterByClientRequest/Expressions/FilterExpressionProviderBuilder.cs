// <copyright file="FilterExpressionProviderBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class FilterExpressionProviderBuilder<TEntity> : IFilterExpressionProviderBuilder<TEntity>
    {
        private readonly IDictionary<FilterType, Expression<Func<TEntity, bool>>> filters =
            new Dictionary<FilterType, Expression<Func<TEntity, bool>>>();

        public IFilterExpressionProviderBuilder<TEntity> AddEquals(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.Equals, expression);

        public IFilterExpressionProviderBuilder<TEntity> AddContains(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.Contains, expression);

        public IFilterExpressionProviderBuilder<TEntity> AddGreaterThan(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.GreaterThan, expression);

        public IFilterExpressionProviderBuilder<TEntity> AddGreaterThanOrEqual(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.GreaterThanOrEqual, expression);

        public IFilterExpressionProviderBuilder<TEntity> AddLessThan(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.LessThan, expression);

        public IFilterExpressionProviderBuilder<TEntity> AddLessThanOrEqual(
            Expression<Func<TEntity, bool>> expression) =>
            this.Add(FilterType.LessThanOrEqual, expression);

        public IDictionary<FilterType, Expression<Func<TEntity, bool>>> Build() => this.filters;

        private IFilterExpressionProviderBuilder<TEntity> Add(
            FilterType type, Expression<Func<TEntity, bool>> expression)
        {
            this.filters[type] = expression;
            return this;
        }
    }
}