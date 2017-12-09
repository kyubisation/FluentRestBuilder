// <copyright file="OtherEntity.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.EntityFramework
{
    using System;

    public class OtherEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public double Rate { get; set; }

        public int? IntValue { get; set; }

        public bool Active { get; set; }

        public bool? Status { get; set; }
    }
}
