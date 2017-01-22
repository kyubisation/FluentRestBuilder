// <copyright file="NonNullablePropertyMappingTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping.Reflection
{
    using System.Reflection;
    using HypertextApplicationLanguage.Mapping.Reflection;
    using Mocks;
    using Xunit;

    public class NonNullablePropertyMappingTest
    {
        [Fact]
        public void TestNonNullableToNonNullableMapping()
        {
            var mapping = this.CreateMapping(nameof(Source.Id), nameof(Target.Id));
            var source = new Source { Id = 1 };
            var target = new Target { Id = 0 };
            mapping.MapProperty(source, target);
            Assert.Equal(source.Id, target.Id);
        }

        [Fact]
        public void TestNullableWithValueToNonNullableMapping()
        {
            var mapping = this.CreateMapping(nameof(Source.Id2), nameof(Target.Id2));
            var source = new Source { Id2 = 1 };
            var target = new Target { Id2 = 0 };
            mapping.MapProperty(source, target);
            Assert.Equal(source.Id2, target.Id2);
        }

        [Fact]
        public void TestNullValueToNonNullableMapping()
        {
            var mapping = this.CreateMapping(nameof(Source.Id2), nameof(Target.Id2));
            const int targetValue = 10;
            var source = new Source { Id2 = null };
            var target = new Target { Id2 = targetValue };
            mapping.MapProperty(source, target);
            Assert.Equal(targetValue, target.Id2);
        }

        private NonNullablePropertyMapping<Source, Target> CreateMapping(
            string inputProperty, string outputProperty)
        {
            var sourceId = typeof(Source).GetProperty(inputProperty);
            var targetId = typeof(Target).GetProperty(outputProperty);
            return new NonNullablePropertyMapping<Source, Target>(sourceId, targetId);
        }
    }
}
