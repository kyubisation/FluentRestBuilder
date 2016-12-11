// <copyright file="DbSetLinqTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.RestCollectionMutators
{
    using System;
    using System.Linq;
    using Mocks;
    using Xunit;

    public class DbSetLinqTest : IDisposable
    {
        private MockDbContext context;

        public DbSetLinqTest()
        {
            var options = MockDbContext.ConfigureInMemoryContextOptions();
            MockDbContext.CreateEntities(options);

            this.context = new MockDbContext(options);
        }

        [Fact]
        public void TestTakeOrderFilter()
        {
            var result = this.context.Entities
                .Take(5)
                .OrderByDescending(e => e.Id)
                .Where(e => e.Id > 5)
                .ToList();
            Assert.Equal(0, result.Count);
        }

        public void Dispose()
        {
            if (this.context == null)
            {
                return;
            }

            this.context.Dispose();
            this.context = null;
        }
    }
}
