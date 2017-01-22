// <copyright file="NonNullablePropertyMapping.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    using System.Reflection;

    public class NonNullablePropertyMapping<TInput, TOutput> : PropertyMapping<TInput, TOutput>
    {
        public NonNullablePropertyMapping(PropertyInfo inputProperty, PropertyInfo outputProperty)
            : base(inputProperty, outputProperty)
        {
        }

        public override void MapProperty(TInput input, TOutput output)
        {
            var value = this.GetValue(input);
            if (value != null)
            {
                this.SetValue(output, value);
            }
        }
    }
}
