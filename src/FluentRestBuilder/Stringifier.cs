// <copyright file="Stringifier.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Stringifier
    {
        public static string Convert<TType>(TType value)
        {
            var properties = TypeDescriptor.GetProperties(value)
                .Cast<PropertyDescriptor>()
                .Select(p => $"{p.Name}: {p.GetValue(value)}")
                .Aggregate((current, next) => $"{current}, {next}");
            return $"{value.GetType().Name} {{{properties}}}";
        }

        public static string ToPropertyName<TSource, TProperty>(
            this Expression<Func<TSource, TProperty>> expression)
        {
            var type = typeof(TSource);
            var member = expression?.Body as MemberExpression;
            var propertyInfo = member?.Member as PropertyInfo;
            if (propertyInfo == null || !propertyInfo.ReflectedType.IsAssignableFrom(type))
            {
                throw new ArgumentException(
                    "The provided expression is not a valid property expression!",
                    nameof(expression));
            }

            return propertyInfo.Name;
        }
    }
}
