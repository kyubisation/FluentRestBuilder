// <copyright file="Entity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks.EntityFramework
{
    using System.Collections.Generic;
    using System.Linq;

    public class Entity
    {
        public static IReadOnlyCollection<Entity> Entities { get; } =
            Enumerable.Range(1, 10)
                .Select(id => new Entity
                {
                    Id = id,
                    Name = $"name{id}",
                    Description = $"description{id}"
                })
                .ToList()
                .AsReadOnly();

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}