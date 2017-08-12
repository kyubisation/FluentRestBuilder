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
                        Name = $"Name {keys.Item1} {keys.Item2}",
                    })
                    .ToList();
                multiKeyEntities
                    .Aggregate((DbContext)context, (current, next) => current.Add(next).Context)
                    .SaveChanges();
                return multiKeyEntities;
            }
        }

        public List<Parent> CreateParentsWithChildren(int amount)
        {
            var childCounter = 0;
            var random = new Random();
            return this.CreateEntities(
                amount,
                i => new Parent
            {
                Id = i,
                Name = $"name{i}",
                Children = Enumerable.Range(1, random.Next(3, 5))
                    .Select(k => new Child
                    {
                        Id = ++childCounter,
                        Name = $"child name {i} {k}",
                    })
                    .ToList(),
            });
        }

        public List<Entity> CreateEnumeratedEntities(int amount) =>
            this.CreateEntities(amount, i => CreateEntity(i, $"Name {i}", $"Description {i}"));

        public List<Entity> CreateSimilarEntities(
            int amount, string name = "Name", string description = "Description") =>
            this.CreateEntities(amount, i => CreateEntity(i, name, description));

        public List<OtherEntity> CreateOtherEntities(int amount)
        {
            return this.CreateEntities(
                amount,
                i => new OtherEntity
            {
                Id = i,
                Name = $"Name {i}",
                Description = $"Description {i}",
                Rate = 0.11 + i,
                CreatedOn = new DateTime(2017, 1, i, 0, 0, 0),
            });
        }

        private static Entity CreateEntity(int id, string name, string description) =>
            new Entity
            {
                Id = id,
                Name = name,
                Description = description,
            };

        private List<TEntity> CreateEntities<TEntity>(int amount, Func<int, TEntity> factory)
            where TEntity : class
        {
            using (var context = this.Create())
            {
                var newId = context.Entities.Count() + 1;
                var entities = Enumerable.Range(newId, amount)
                    .Select(factory)
                    .ToList();
                entities
                    .Aggregate((DbContext)context, (current, next) => current.Add(next).Context)
                    .SaveChanges();
                return entities;
            }
        }
    }
}
