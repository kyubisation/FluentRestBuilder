// <copyright file="PersistantDatabase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class PersistantDatabase
    {
        private readonly DbContextOptions<MockDbContext> options =
                MockDbContext.ConfigureInMemoryContextOptions();

        public MockDbContext Create() => new MockDbContext(this.options);

        public List<Entity> CreateEnumeratedEntities(int amount) =>
            this.CreateEntities(amount, i => CreateEntity(i, $"Name {i}", $"Description {i}"));

        public List<Entity> CreateSimilarEntities(
            int amount, string name = "Name", string description = "Description") =>
            this.CreateEntities(amount, i => CreateEntity(i, name, description));

        private static Entity CreateEntity(int id, string name, string description) =>
            new Entity
            {
                Id = id,
                Name = name,
                Description = description
            };

        private List<Entity> CreateEntities(int amount, Func<int, Entity> callback)
        {
            using (var context = this.Create())
            {
                var entities = Enumerable.Range(1, amount)
                    .Select(callback)
                    .ToList();
                entities.ForEach(e => context.Add(e));
                context.SaveChanges();
                return entities;
            }
        }
    }
}
