// <copyright file="NullablePropertyMapping.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    using System.Reflection;

    public class NullablePropertyMapping<TInput, TOutput> : PropertyMapping<TInput, TOutput>
    {
        public NullablePropertyMapping(PropertyInfo inputProperty, PropertyInfo outputProperty)
            : base(inputProperty, outputProperty)
        {
        }

        public override void MapProperty(TInput input, TOutput output)
        {
            var value = this.GetValue(input);
            this.SetValue(output, value);
        }
    }
}
