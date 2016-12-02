namespace KyubiCode.FluentRest.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Filters;
    using Microsoft.EntityFrameworkCore.Metadata;
    using OrderBy;

    public interface IExpressionFactory<TEntity>
    {
        IEnumerable<IFilterBuilder<TEntity>> PrimaryKeyFilters { get; }

        IDictionary<string, IFilterBuilder<TEntity>> FilterExpressions { get; }

        IEnumerable<IOrderByBuilder<TEntity>> OrderByExpressions { get; }

        IEnumerable<IProperty> DeclaredProperties { get; }

        Expression<Func<TEntity, bool>> CreatePrimaryKeyFilterExpression(IEnumerable<object> keys);

        Expression<Func<TEntity, bool>> JoinExpressionsByOr(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions);

        Expression<Func<TEntity, bool>> JoinExpressionsByAnd(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions);
    }
}