// <copyright file="NullablePropertyMappingTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping.Reflection
{
    using System.Reflection;
    using HypertextApplicationLanguage.Mapping.Reflection;
    using Mocks;
    using Xunit;

    public class NullablePropertyMappingTest
    {
        [Fact]
        public void TestNonNullableToNullableMapping()
        {
            var mapping = this.CreateMapping(nameof(Source.Id3), nameof(Target.Id3));
            var source = new Source { Id3 = 1 };
            var target = new Target { Id3 = 0 };
            mapping.MapProperty(source, target);
            Assert.Equal(source.Id3, target.Id3);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 1)]
        [InlineData(1, null)]
        [InlineData(1, 2)]
        public void TestMappings(int? sourceValue, int? targetValue)
        {
            var mapping = this.CreateMapping(nameof(Source.Id4), nameof(Target.Id4));
            var source = new Source { Id4 = sourceValue };
            var target = new Target { Id4 = targetValue };
            mapping.MapProperty(source, target);
            Assert.Equal(source.Id4, target.Id4);
        }

        private NullablePropertyMapping<Source, Target> CreateMapping(
            string inputProperty, string outputProperty)
        {
            var sourceId = typeof(Source).GetProperty(inputProperty);
            var targetId = typeof(Target).GetProperty(outputProperty);
            return new NullablePropertyMapping<Source, Target>(sourceId, targetId);
        }
    }
}
