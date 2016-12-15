// <copyright file="IRestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.CollectionTransformation
{
    using System.Collections.Generic;
    using Common;
    using Hal;

    public interface IRestCollectionLinkGenerator
    {
        IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo);
    }
}
