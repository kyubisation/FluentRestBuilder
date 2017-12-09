// <copyright file="DbSetLinqTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test
{
    using System;
    using System.Linq;
    using Mocks.EntityFramework;
    using Xunit;

    public class DbSetLinqTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private MockDbContext context;

        public DbSetLinqTest()
        {
            this.database = new PersistantDatabase();
            this.context = this.database.Create();
        }

        [Fact]
        public void TestTakeOrderFilter()
        {
            this.database.CreateEnumeratedEntities(10);
            var result = this.context.Entities
                .Take(5)
                .OrderByDescending(e => e.Id)
                .Where(e => e.Id > 5)
                .ToList();
            Assert.Empty(result);
        }

        [Fact]
        public void TestOrderByOverwrite()
        {
            using (var localContext = this.database.Create())
            {
                localContext.Add(new Entity { Id = 1, Name = "za" });
                localContext.Add(new Entity { Id = 2, Name = "za" });
                localContext.Add(new Entity { Id = 3, Name = "aa" });
                localContext.SaveChanges();
            }

            var result = this.context.Entities
                .OrderBy(e => e.Name)
                .ThenBy(e => e.Id)
                //// ReSharper disable once MultipleOrderBy
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
