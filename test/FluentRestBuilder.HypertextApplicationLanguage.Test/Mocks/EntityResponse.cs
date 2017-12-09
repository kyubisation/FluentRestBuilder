// <copyright file="EntityResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mocks
{
    using FluentRestBuilder.Mocks.EntityFramework;

    public class EntityResponse : RestEntity
    {
        public EntityResponse(Entity entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Description = entity.Description;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
