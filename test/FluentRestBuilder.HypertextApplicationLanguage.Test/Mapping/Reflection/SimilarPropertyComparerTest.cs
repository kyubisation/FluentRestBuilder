// <copyright file="SimilarPropertyComparerTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping.Reflection
{
    using System.Linq;
    using System.Reflection;
    using HypertextApplicationLanguage.Mapping.Reflection;
    using Mocks;
    using Xunit;

    public class SimilarPropertyComparerTest
    {
        [Theory]
        [InlineData(nameof(Source.Id), nameof(Target.Id))]
        [InlineData(nameof(Source.Id2), nameof(Target.Id2))]
        [InlineData(nameof(Source.Id3), nameof(Target.Id3))]
        [InlineData(nameof(Source.Id4), nameof(Target.Id4))]
        public void TestEqualsTrue(string sourceName, string targetName)
        {
            var sourceProperty = typeof(Source).GetProperty(sourceName);
            var targetProperty = typeof(Source).GetProperty(targetName);
            var comparer = new SimilarPropertyComparer();
            Assert.True(comparer.Equals(sourceProperty, targetProperty));
        }

        [Theory]
        [InlineData(nameof(Source.Id), nameof(Target.Id2))]
        [InlineData(nameof(Source.Id4), nameof(Target.Id3))]
        [InlineData(nameof(Source.Name), nameof(Target.Name))]
        public void TestEqualsFalse(string sourceName, string targetName)
        {
            var sourceProperty = typeof(Source).GetProperty(sourceName);
            var targetProperty = typeof(Target).GetProperty(targetName);
            var comparer = new SimilarPropertyComparer();
            Assert.False(comparer.Equals(sourceProperty, targetProperty));
        }

        [Fact]
        public void TestComparison()
        {
            var sourceProperties = typeof(Source)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var targetProperties = typeof(Target)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var result = sourceProperties
                .Intersect(targetProperties, new SimilarPropertyComparer())
                .ToList();
            Assert.Equal(4, result.Count);
            Assert.All(result, p => Assert.True(p.Name.StartsWith("Id")));
        }

        [Fact]
        public void TestNullHashCode()
        {
            Assert.Equal(0, new SimilarPropertyComparer().GetHashCode(null));
        }
    }
}
