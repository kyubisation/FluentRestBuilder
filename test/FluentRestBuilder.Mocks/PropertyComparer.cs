// <copyright file="PropertyComparer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class PropertyComparer<TComparable> : EqualityComparer<TComparable>
    {
        private readonly List<PropertyInfo> properties;

        public PropertyComparer()
        {
            this.properties = typeof(TComparable)
                .GetRuntimeProperties()
                .Where(p => p.GetGetMethod(true).IsPublic)
                .ToList();
        }

        public override bool Equals(TComparable x, TComparable y) =>
            this.properties.All(i => Equals(i, x, y));

        public override int GetHashCode(TComparable obj) =>
            this.properties
                .Select(p => $"{p.Name}: ${p.GetValue(obj)}")
                .Aggregate(
                    $"{typeof(TComparable).Name} {{",
                    (current, next) => $"{current}, {next}",
                    r => $"{r}}}")
                .GetHashCode();

        private static bool Equals(PropertyInfo info, TComparable x, TComparable y)
        {
            if (!info.PropertyType.IsValueType && info.PropertyType != typeof(string))
            {
                return true;
            }

            return info.GetValue(x)?.Equals(info.GetValue(y)) ?? info.GetValue(y) == null;
        }
    }
}
