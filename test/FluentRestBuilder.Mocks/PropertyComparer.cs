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
        private readonly PropertyInfo[] properties;

        public PropertyComparer()
        {
            this.properties = typeof(TComparable).GetProperties(BindingFlags.Public);
        }

        public override bool Equals(TComparable x, TComparable y) =>
            this.properties.All(i => i.GetValue(x) == i.GetValue(y));

        public override int GetHashCode(TComparable obj) =>
            this.properties
                .Aggregate(string.Empty, (current, next) => $"{current}:{next}")
                .GetHashCode();
    }
}
