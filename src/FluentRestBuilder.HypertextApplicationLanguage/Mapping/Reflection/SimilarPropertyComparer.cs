// <copyright file="SimilarPropertyComparer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class SimilarPropertyComparer : EqualityComparer<PropertyInfo>
    {
        public override bool Equals(PropertyInfo x, PropertyInfo y) =>
            x.Name == y.Name && EqualTruePropertyType(x, y);

        public override int GetHashCode(PropertyInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }

            var propertyType = GetTruePropertyType(obj.PropertyType);
            return $"{obj.Name}:{propertyType.FullName}"
                .GetHashCode();
        }

        private static bool EqualTruePropertyType(PropertyInfo x, PropertyInfo y) =>
            GetTruePropertyType(x.PropertyType) == GetTruePropertyType(y.PropertyType);

        private static Type GetTruePropertyType(Type propertyType)
        {
            if (propertyType.GetTypeInfo().IsGenericType
                && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(propertyType);
            }

            return propertyType;
        }
    }
}
