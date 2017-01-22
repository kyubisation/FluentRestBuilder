// <copyright file="ReflectionMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ReflectionMapper<TInput, TOutput> : IReflectionMapper<TInput, TOutput>
        where TOutput : new()
    {
        private readonly Type inputType = typeof(TInput);
        private readonly Type outputType = typeof(TOutput);
        private readonly List<PropertyMapping<TInput, TOutput>> propertyMappings;

        public ReflectionMapper()
        {
            this.propertyMappings = this.CreatePropertyMappings();
        }

        public TOutput Map(TInput input)
        {
            var output = new TOutput();
            this.propertyMappings.ForEach(m => m.MapProperty(input, output));
            return output;
        }

        private List<PropertyMapping<TInput, TOutput>> CreatePropertyMappings()
        {
            var inputProperties = this.inputType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var outputProperties = this.outputType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return inputProperties
                .Intersect(outputProperties, new SimilarPropertyComparer())
                .Select(p => this.CreatePropertyMapping(p.Name))
                .ToList();
        }

        private PropertyMapping<TInput, TOutput> CreatePropertyMapping(string propertyName)
        {
            return this.CreatePropertyMapping(
                this.inputType.GetProperty(propertyName),
                this.outputType.GetProperty(propertyName));
        }

        private PropertyMapping<TInput, TOutput> CreatePropertyMapping(
            PropertyInfo inputProperty, PropertyInfo outputProperty)
        {
            if (this.IsNullableProperty(outputProperty))
            {
                return new NullablePropertyMapping<TInput, TOutput>(inputProperty, outputProperty);
            }

            return new NonNullablePropertyMapping<TInput, TOutput>(inputProperty, outputProperty);
        }

        private bool IsNullableProperty(PropertyInfo property) =>
            property.PropertyType.GetTypeInfo().IsGenericType
                && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}