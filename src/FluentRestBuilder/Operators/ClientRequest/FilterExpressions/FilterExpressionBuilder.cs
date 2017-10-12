// <copyright file="FilterExpressionBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.FilterExpressions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class FilterExpressionBuilder<TEntity, TFilter>
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly MethodInfo ContainsMethod =
            typeof(string).GetMethod("Contains", new[] { typeof(string) });

        private static readonly MethodInfo StartsWithMethod =
            typeof(string).GetMethod("StartsWith", new[] { typeof(string) });

        private static readonly MethodInfo EndsWithMethod =
            typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        //// ReSharper restore StaticMemberInGenericType

        private readonly MemberExpression property;
        private readonly ConstantExpression filter;
        private readonly ParameterExpression parameter;

        public FilterExpressionBuilder(string propertyName, TFilter filter)
        {
            this.parameter = Expression.Parameter(typeof(TEntity));
            this.property = Expression.Property(this.parameter, propertyName);
            this.filter = Expression.Constant(filter, typeof(TFilter));
        }

        public Expression<Func<TEntity, bool>> CreateEqualsExpression() =>
            this.ToLambda(Expression.Equal(this.property, this.filter));

        public Expression<Func<TEntity, bool>> CreateNotEqualExpression() =>
            this.ToLambda(Expression.NotEqual(this.property, this.filter));

        public Expression<Func<TEntity, bool>> CreateContainsExpression() =>
            this.ToLambda(Expression.Call(this.property, ContainsMethod, this.filter));

        public Expression<Func<TEntity, bool>> CreateStartsWithExpression() =>
            this.ToLambda(Expression.Call(this.property, StartsWithMethod, this.filter));

        public Expression<Func<TEntity, bool>> CreateEndsWithExpression() =>
            this.ToLambda(Expression.Call(this.property, EndsWithMethod, this.filter));

        public Expression<Func<TEntity, bool>> CreateGreaterThanExpression() =>
            this.ToLambda(Expression.GreaterThan(this.property, this.filter));

        public Expression<Func<TEntity, bool>> CreateGreaterThanOrEqualExpression() =>
            this.ToLambda(Expression.GreaterThanOrEqual(this.property, this.filter));

        public Expression<Func<TEntity, bool>> CreateLessThanExpression() =>
            this.ToLambda(Expression.LessThan(this.property, this.filter));

        public Expression<Func<TEntity, bool>> CreateLessThanOrEqualExpression() =>
            this.ToLambda(Expression.LessThanOrEqual(this.property, this.filter));

        private Expression<Func<TEntity, bool>> ToLambda(Expression expression) =>
            Expression.Lambda<Func<TEntity, bool>>(expression, this.parameter);
    }
}
