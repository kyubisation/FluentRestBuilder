// <copyright file="FilterExpressionDictionary.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Interpreters.Requests;

    public class FilterExpressionDictionary<TEntity> : Dictionary<FilterType, Expression<Func<TEntity, bool>>>
    {
        public FilterExpressionDictionary<TEntity> AddEquals(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.Equals, expression);

        public FilterExpressionDictionary<TEntity> AddContains(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.Contains, expression);

        public FilterExpressionDictionary<TEntity> AddGreaterThan(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.GreaterThan, expression);

        public FilterExpressionDictionary<TEntity> AddGreaterThanOrEqual(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.GreaterThanOrEqual, expression);

        public FilterExpressionDictionary<TEntity> AddLessThan(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.LessThan, expression);

        public FilterExpressionDictionary<TEntity> AddLessThanOrEqual(
            Expression<Func<TEntity, bool>> expression) =>
            this.AddInternal(FilterType.LessThanOrEqual, expression);

        private FilterExpressionDictionary<TEntity> AddInternal(
            FilterType filterType,
            Expression<Func<TEntity, bool>> expression)
        {
            this[filterType] = expression;
            return this;
        }
    }
}
