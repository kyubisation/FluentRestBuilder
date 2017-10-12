// <copyright file="NumericFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FilterConverters;
    using Interpreters.Requests;

    public class NumericFilterExpressionProvider<TEntity, TFilter>
        : GenericFilterExpressionProvider<TEntity, TFilter>
    {
        public NumericFilterExpressionProvider(
            string propertyName, IFilterToTypeConverter<TFilter> converter)
            : base(filter => CreateDefault(propertyName, filter), converter)
        {
        }

        private static IDictionary<FilterType, Expression<Func<TEntity, bool>>> CreateDefault(
            string propertyName, TFilter filter)
        {
            var builder = new FilterExpressionBuilder<TEntity, TFilter>(propertyName, filter);
            return new Dictionary<FilterType, Expression<Func<TEntity, bool>>>
            {
                [FilterType.Default] = builder.CreateEqualsExpression(),
                [FilterType.Equals] = builder.CreateEqualsExpression(),
                [FilterType.NotEqual] = builder.CreateNotEqualExpression(),
                [FilterType.GreaterThan] = builder.CreateGreaterThanExpression(),
                [FilterType.GreaterThanOrEqual] = builder.CreateGreaterThanOrEqualExpression(),
                [FilterType.LessThan] = builder.CreateLessThanExpression(),
                [FilterType.LessThanOrEqual] = builder.CreateLessThanOrEqualExpression(),
            };
        }
    }
}
