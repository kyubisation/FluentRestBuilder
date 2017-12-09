// <copyright file="BooleanFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FilterConverters;
    using Interpreters.Requests;

    public class BooleanFilterExpressionProvider<TEntity, TBoolean> : GenericFilterExpressionProvider<TEntity, TBoolean>
    {
        public BooleanFilterExpressionProvider(
            string propertyName, IFilterToTypeConverter<TBoolean> converter)
            : base(filter => CreateDefault(propertyName, filter), converter)
        {
        }

        private static IDictionary<FilterType, Expression<Func<TEntity, bool>>> CreateDefault(
            string propertyName, TBoolean filter)
        {
            var builder = new FilterExpressionBuilder<TEntity, TBoolean>(propertyName, filter);
            return new Dictionary<FilterType, Expression<Func<TEntity, bool>>>
            {
                [FilterType.Default] = builder.CreateEqualsExpression(),
                [FilterType.Equals] = builder.CreateEqualsExpression(),
                [FilterType.NotEqual] = builder.CreateNotEqualExpression(),
            };
        }
    }
}
