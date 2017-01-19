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

        public List<MultiKeyEntity> CreateMultiKeyEntities(int amount)
        {
            using (var context = this.Create())
            {
                var multiKeyEntities = Enumerable.Range(1, amount)
                    .Select(i => Tuple.Create(i, amount + 1 - i))
                    .Select(keys => new MultiKeyEntity
                    {
                        FirstId = keys.Item1,
                        SecondId = keys.Item2,
                        Name = $"Name {keys.Item1} {keys.Item2}"
                    })
                    .ToList();
                multiKeyEntities.ForEach(e => context.Add(e));
                context.SaveChanges();
                return multiKeyEntities;
            }
        }

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
