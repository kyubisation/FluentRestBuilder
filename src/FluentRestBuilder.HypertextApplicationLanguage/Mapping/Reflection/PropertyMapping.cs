// <copyright file="PropertyMapping.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    using System.Reflection;

    public abstract class PropertyMapping<TInput, TOutput>
    {
        private readonly PropertyInfo inputProperty;
        private readonly PropertyInfo outputProperty;

        protected PropertyMapping(
            PropertyInfo inputProperty,
            PropertyInfo outputProperty)
        {
            this.inputProperty = inputProperty;
            this.outputProperty = outputProperty;
        }

        public abstract void MapProperty(TInput input, TOutput output);

        protected object GetValue(TInput input) => this.inputProperty.GetValue(input);

        protected void SetValue(TOutput output, object value) =>
            this.outputProperty.SetValue(output, value);
    }
}