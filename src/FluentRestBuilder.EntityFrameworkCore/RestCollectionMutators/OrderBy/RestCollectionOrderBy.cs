// <copyright file="RestCollectionOrderBy.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.OrderBy
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.RestCollectionMutators.OrderBy;
    using MetaModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public class RestCollectionOrderBy<TEntity> : IRestCollectionOrderBy<TEntity>
    {
        private readonly IExpressionFactory<TEntity> expressionFactory;
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public RestCollectionOrderBy(
            IQueryCollection queryCollection,
            IExpressionFactory<TEntity> expressionFactory,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryCollection = queryCollection;
            this.expressionFactory = expressionFactory;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
        {
            var orderByValue = this.queryCollection[this.queryArgumentKeys.OrderBy];
            return StringValues.IsNullOrEmpty(orderByValue)
                ? queryable : this.OrderBy(queryable, orderByValue);
        }

        private IQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable, StringValues orderByValues)
        {
            if (orderByValues.Count == 1)
            {
                return this.FindOrderByBuilder(queryable, orderByValues.ToString()) ?? queryable;
            }

            return orderByValues.Aggregate<string, IOrderedQueryable<TEntity>>(
                null,
                (current, orderByValue) => current == null
                    ? this.FindOrderByBuilder(queryable, orderByValue)
                    : this.FindOrderByBuilder(current, orderByValue));
        }

        private IOrderedQueryable<TEntity> FindOrderByBuilder(
            IQueryable<TEntity> queryable, string orderBy)
        {
            var expression = this.FindOrderByExpression(orderBy);
            if (expression == null)
            {
                return null;
            }

            return expression.Item1 == OrderByDirection.Ascending
                ? queryable.OrderBy(expression.Item2)
                : queryable.OrderByDescending(expression.Item2);
        }

        private IOrderedQueryable<TEntity> FindOrderByBuilder(
            IOrderedQueryable<TEntity> queryable, string orderBy)
        {
            var expression = this.FindOrderByExpression(orderBy);
            if (expression == null)
            {
                return queryable;
            }

            return expression.Item1 == OrderByDirection.Ascending
                ? queryable.ThenBy(expression.Item2)
                : queryable.ThenByDescending(expression.Item2);
        }

        private Tuple<OrderByDirection, Expression<Func<TEntity, object>>> FindOrderByExpression(
            string orderBy)
        {
            return (
                from orderByBuilder
                in this.expressionFactory.OrderByExpressions
                let direction = orderByBuilder.ResolveDirection(orderBy)
                where direction != OrderByDirection.None
                select Tuple.Create(direction, orderByBuilder.CreateOrderBy()))
                .FirstOrDefault();
        }
    }
}