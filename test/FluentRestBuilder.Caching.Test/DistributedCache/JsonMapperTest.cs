// <copyright file="JsonMapperTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Test.DistributedCache
{
    using System.Text;
    using Caching.DistributedCache;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class JsonMapperTest
    {
        [Fact]
        public void TestMapping()
        {
            var entity = new Entity
            {
                Id = 3,
                Name = "name",
                Description = "descriptionéö",
            };
            var mapper = new JsonMapper<Entity>();
            var bytes = mapper.ToByteArray(entity);
            var convertedEntity = mapper.FromByteArray(bytes);
            Assert.Equal(entity, convertedEntity, new PropertyComparer<Entity>());
        }

        [Fact]
        public void ParseBrokenJson()
        {
            var mapper = new JsonMapper<Entity>();
            var bytes = Encoding.UTF8.GetBytes("{ asdf: ");
            Assert.Throws<MappingException>(() => mapper.FromByteArray(bytes));
        }
    }
}
