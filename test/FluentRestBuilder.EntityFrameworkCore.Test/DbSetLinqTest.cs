// <copyright file="DbSetLinqTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Test.Common.Mocks.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class DbSetLinqTest : IDisposable
    {
        private readonly DbContextOptions<MockDbContext> options;
        private MockDbContext context;

        public DbSetLinqTest()
        {
            this.options = MockDbContext.ConfigureInMemoryContextOptions();

            this.context = new MockDbContext(this.options);
        }

        [Fact]
        public void TestTakeOrderFilter()
        {
            MockDbContext.CreateEntities(this.options);
            var result = this.context.Entities
                .Take(5)
                .OrderByDescending(e => e.Id)
                .Where(e => e.Id > 5)
                .ToList();
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void TestOrderByOverwrite()
        {
            using (var localContext = new MockDbContext(this.options))
            {
                localContext.Add(new Entity { Id = 1, Name = "za" });
                localContext.Add(new Entity { Id = 2, Name = "za" });
                localContext.Add(new Entity { Id = 3, Name = "aa" });
                localContext.SaveChanges();
            }

            var result = this.context.Entities
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Id)
                .OrderByDescending(e => e.Name)
                .ThenByDescending(e => e.Id)
                .ToList();
            Assert.Equal(result.Select(e => e.Id), new[] { 2, 1, 3 });
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
