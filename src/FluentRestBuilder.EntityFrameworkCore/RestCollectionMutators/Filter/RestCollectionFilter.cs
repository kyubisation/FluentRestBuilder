// <copyright file="RestCollectionFilter.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.RestCollectionMutators.Filter;
    using MetaModel;
    using MetaModel.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;

    public class RestCollectionFilter<TEntity> : IRestCollectionFilter<TEntity>
    {
        private readonly IExpressionFactory<TEntity> expressionFactory;
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly IQueryCollection queryCollection;

        public RestCollectionFilter(
            IHttpContextAccessor contextAccessor,
            IExpressionFactory<TEntity> expressionFactory,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryCollection = contextAccessor.HttpContext.Request.Query;
            this.expressionFactory = expressionFactory;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
        {
            var filterValue = this.queryCollection[this.queryArgumentKeys.Filter];
            return StringValues.IsNullOrEmpty(filterValue)
                ? queryable : this.Filter(queryable, filterValue);
        }

        private IQueryable<TEntity> Filter(IQueryable<TEntity> queryable, StringValues filterValues)
        {
            try
            {
                var filters = this.DeserializeFilters(filterValues);
                return this.Filter(queryable, filters);
            }
            catch (JsonSerializationException)
            {
                return queryable.Where(e => false);
            }
        }

        private IDictionary<string, string> DeserializeFilters(StringValues filterValues)
        {
            return filterValues.ToArray()
                .SelectMany(JsonConvert.DeserializeObject<Dictionary<string, string>>)
                .ToLookup(p => p.Key, p => p.Value)
                .ToDictionary(p => p.Key, p => p.Last(), StringComparer.OrdinalIgnoreCase);
        }

        private IQueryable<TEntity> Filter(
            IQueryable<TEntity> queryable, IDictionary<string, string> filterValues)
        {
            var expressions = this.expressionFactory.FilterExpressions
                .Select(e => this.FindFilterForExpression(e.Key, e.Value, filterValues))
                .Where(e => e != null);
            var expression = this.expressionFactory.JoinExpressionsByAnd(expressions);
            return queryable.Where(expression);
        }

        private Expression<Func<TEntity, bool>> FindFilterForExpression(
            string key, IFilterBuilder<TEntity> builder, IDictionary<string, string> filterValues)
        {
            string filter;
            return filterValues.TryGetValue(key, out filter)
                ? builder.CreateFilter(filter) : null;
        }
    }
}