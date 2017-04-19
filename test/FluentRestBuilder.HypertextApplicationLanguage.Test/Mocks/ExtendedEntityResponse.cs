// <copyright file="ExtendedEntityResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mocks
{
    using System.Collections.Generic;
    using FluentRestBuilder.Mocks.EntityFramework;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Mvc;

    public class ExtendedEntityResponse : EntityResponse, ILinkGenerator<Entity>
    {
        public IEnumerable<NamedLink> GenerateLinks(IUrlHelper urlHelper, Entity entity)
        {
            return new[]
            {
                new LinkToSelf(new Link($"/{entity.Id}")),
                new NamedLink("asdf", new Link("/asdf")),
                new NamedLink("qwer", new Link("/qwer")),
            };
        }
    }
}
