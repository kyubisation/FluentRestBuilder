// <copyright file="MultiKeyEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.EntityFramework
{
    using System.Collections.Generic;
    using System.Linq;

    public class MultiKeyEntity
    {
        public static IReadOnlyCollection<MultiKeyEntity> Entities { get; } =
            Enumerable.Range(1, 3)
                .SelectMany(id => Enumerable.Range(1, 3)
                    .Select(id2 => new MultiKeyEntity
                    {
                        FirstId = id,
                        SecondId = id2,
                        Name = $"name{id}_{id2}"
                    }))
                .ToList()
                .AsReadOnly();

        public int FirstId { get; set; }

        public int SecondId { get; set; }

        public string Name { get; set; }
    }
}
