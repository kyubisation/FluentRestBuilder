// <copyright file="ReflectionMapperTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping.Reflection
{
    using HypertextApplicationLanguage.Mapping.Reflection;
    using Mocks;
    using Xunit;

    public class ReflectionMapperTest
    {
        [Fact]
        public void TestMapping()
        {
            var mapper = new ReflectionMapper<Source, Target>();
            var source = new Source
            {
                Id = 1,
                Id2 = 2,
                Id3 = 3,
                Id4 = 4,
                Name = "name",
            };
            var target = mapper.Map(source);
            Assert.Equal(source.Id, target.Id);
            Assert.Equal(source.Id2, target.Id2);
            Assert.Equal(source.Id3, target.Id3);
            Assert.Equal(source.Id4, target.Id4);
            Assert.Null(target.Name);
        }
    }
}
