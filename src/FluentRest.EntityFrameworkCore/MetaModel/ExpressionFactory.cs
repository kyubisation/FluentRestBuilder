// <copyright file="ExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Linq.Expressions;
    using Filters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.EntityFrameworkCore.Metadata;
    using OrderBy;

    public class ExpressionFactory<TEntity> : IExpressionFactory<TEntity>
    {
        private readonly IModelContainer<TEntity> modelContainer;

        public ExpressionFactory(IModelContainer<TEntity> modelContainer)
        {
            this.modelContainer = modelContainer;
            this.PrimaryKeyFilters = this.modelContainer.PrimaryKey.Properties
                .Select(SelectPrimaryKeyBuilder)
                .ToImmutableList();
            this.FilterExpressions = this.modelContainer.Properties
                .ToDictionary(p => p.Name, SelectFilterBuilder);
            this.OrderByExpressions = this.modelContainer.Properties
                .Select(SelectOrderByBuilder)
                .ToImmutableList();
            this.DeclaredProperties = this.modelContainer.Properties
                .Where(p => !p.IsShadowProperty && !p.IsForeignKey())
                .ToImmutableList();
        }

        public IEnumerable<IFilterBuilder<TEntity>> PrimaryKeyFilters { get; }

        public IDictionary<string, IFilterBuilder<TEntity>> FilterExpressions { get; }

        public IEnumerable<IOrderByBuilder<TEntity>> OrderByExpressions { get; }

        public IEnumerable<IProperty> DeclaredProperties { get; }

        public Expression<Func<TEntity, bool>> CreatePrimaryKeyFilterExpression(
            IEnumerable<object> keys)
        {
            var keyList = keys.ToList();
            this.AssertPrimaryKeyTypeParity(keyList.Select(k => k.GetType()).ToList());
            return this.PrimaryKeyFilters
                .Zip(keyList, (b, k) => b.CreateFilter(k))
                .Aggregate(And);
        }

        public Expression<Func<TEntity, bool>> JoinExpressionsByOr(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions) => expressions.Aggregate(Or);

        public Expression<Func<TEntity, bool>> JoinExpressionsByAnd(
            IEnumerable<Expression<Func<TEntity, bool>>> expressions) => expressions.Aggregate(And);

        private static IFilterBuilder<TEntity> SelectPrimaryKeyBuilder(IProperty property)
        {
            if (property.ClrType == typeof(string))
            {
                return new StringPropertyEqualsFilterBuilder<TEntity>(property);
            }

            return new ValueTypePropertyEqualsFilterBuilder<TEntity>(property);
        }

        private static IFilterBuilder<TEntity> SelectFilterBuilder(IProperty property)
        {
            if (property.ClrType == typeof(string))
            {
                return new StringPropertyContainsFilterBuilder<TEntity>(property);
            }

            return new ValueTypePropertyEqualsFilterBuilder<TEntity>(property);
        }

        private static IOrderByBuilder<TEntity> SelectOrderByBuilder(IProperty property)
        {
            return new PropertyOrderByBuilder<TEntity>(property);
        }

        private static Expression<Func<TEntity, bool>> Or(
            Expression<Func<TEntity, bool>> expr1, Expression<Func<TEntity, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private static Expression<Func<TEntity, bool>> And(
            Expression<Func<TEntity, bool>> expr1, Expression<Func<TEntity, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<TEntity, bool>>(
                Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        private void AssertPrimaryKeyTypeParity(IReadOnlyCollection<Type> types)
        {
            if (this.modelContainer.PrimaryKey.Properties.Count == types.Count &&
                this.modelContainer.PrimaryKey.Properties
                    .Select(p => p.ClrType)
                    .SequenceEqual(types))
            {
                return;
            }

            var primaryKeySequence = this.modelContainer.PrimaryKey.Properties
                .Select(p => p.ClrType.Name)
                .Join();
            throw new PrimaryKeyMismatchException(
                $"Expected {primaryKeySequence} " +
                $"but received {types.Select(k => k.Name).Join()}");
        }
    }
}
