// <copyright file="GenericFilterExpressionProvider.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class GenericFilterExpressionProvider<TEntity, TFilter> : FilterExpressionProvider<TEntity, TFilter>
    {
        private readonly Func<string, TFilter> conversion;

        public GenericFilterExpressionProvider(
            IDictionary<FilterType, Func<TFilter, Expression<Func<TEntity, bool>>>> filterDictionary,
            Func<string, TFilter> conversion)
            : base(filterDictionary)
        {
            this.conversion = conversion ?? ConvertToFilterType;
        }

        public override Expression<Func<TEntity, bool>> Resolve(FilterType type, string filter)
        {
            try
            {
                var filterValue = this.conversion(filter);
                return this.ResolveForType(type, filterValue);
            }
            catch (Exception)
            {
                return e => false;
            }
        }

        private static TFilter ConvertToFilterType(string filter) =>
            (TFilter)Convert.ChangeType(filter, typeof(TFilter));
    }
}