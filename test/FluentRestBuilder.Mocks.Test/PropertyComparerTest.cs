// <copyright file="PropertyComparerTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.Test
{
    using Xunit;

    public class PropertyComparerTest
    {
        [Fact]
        public void TestEqual()
        {
            var entity1 = new Comparable { Id = 1, Name = "Name", Description = "Description" };
            var entity2 = new Comparable { Id = 1, Name = "Name", Description = "Description" };
            var entity3 = new Comparable
            {
                Id = 1,
                Name = "Name",
                Description = "Description",
                Object = new object(),
            };
            Assert.Equal(entity1, entity2, new PropertyComparer<Comparable>());
            Assert.Equal(entity1, entity3, new PropertyComparer<Comparable>());
        }

        [Fact]
        public void TestEqualWithNull()
        {
            var entity1 = new Comparable { Id = 1, Name = "Name" };
            var entity2 = new Comparable { Id = 1, Name = "Name" };
            Assert.Equal(entity1, entity2, new PropertyComparer<Comparable>());
        }

        [Fact]
        public void TestNotEqual()
        {
            var entity1 = new Comparable { Id = 1, Name = "Name 1", Description = "Description" };
            var entity2 = new Comparable { Id = 1, Name = "Name 2", Description = "Description" };
            Assert.NotEqual(entity1, entity2, new PropertyComparer<Comparable>());
        }

        [Fact]
        public void TestNotEqualWithNull()
        {
            var entity1 = new Comparable { Id = 1, Name = "Name", Description = "Description" };
            var entity2 = new Comparable { Id = 1, Name = "Name" };
            Assert.NotEqual(entity1, entity2, new PropertyComparer<Comparable>());
        }

        private class Comparable
        {
            // ReSharper disable UnusedAutoPropertyAccessor.Local
            public int Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public object Object { get; set; }
            //// ReSharper restore UnusedAutoPropertyAccessor.Local
        }
    }
}
