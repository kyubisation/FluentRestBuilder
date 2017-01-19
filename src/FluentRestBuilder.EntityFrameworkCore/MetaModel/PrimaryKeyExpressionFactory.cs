// <copyright file="PrimaryKeyExpressionFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.MetaModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    public class PrimaryKeyExpressionFactory<TEntity> : IPrimaryKeyExpressionFactory<TEntity>
    {
        private readonly IPredicateBuilder<TEntity> predicateBuilder;
        private readonly IKey primaryKey;

        public PrimaryKeyExpressionFactory(
            IModelContainer modelContainer,
            IPredicateBuilder<TEntity> predicateBuilder)
        {
            this.predicateBuilder = predicateBuilder;
            this.primaryKey = modelContainer.Model
                .FindEntityType(typeof(TEntity))
                .FindPrimaryKey();
        }

        public Expression<Func<TEntity, bool>> CreatePrimaryKeyFilterExpression(
            IReadOnlyCollection<object> keys)
        {
            this.AssertPrimaryKeyTypeParity(keys.Select(k => k.GetType()).ToList());
            var expressions = this.primaryKey.Properties.Zip(keys, this.CreateExpression);
            return this.predicateBuilder.JoinExpressionsByAnd(expressions.ToArray());
        }

        private void AssertPrimaryKeyTypeParity(IReadOnlyCollection<Type> types)
        {
            if (this.primaryKey.Properties.Count != types.Count ||
                !this.primaryKey.Properties
                    .Select(p => p.ClrType)
                    .SequenceEqual(types))
            {
                throw new PrimaryKeyMismatchException(this.primaryKey, types);
            }
        }

        private Expression<Func<TEntity, bool>> CreateExpression(
            IProperty property, object keyValue)
        {
            var propertyExpression = Expression.Property(
                this.predicateBuilder.Parameter, property.PropertyInfo);
            var keyConstant = Expression.Constant(keyValue);
            var comparison = Expression.Equal(propertyExpression, keyConstant);
            return Expression.Lambda<Func<TEntity, bool>>(
                comparison, this.predicateBuilder.Parameter);
        }
    }
}