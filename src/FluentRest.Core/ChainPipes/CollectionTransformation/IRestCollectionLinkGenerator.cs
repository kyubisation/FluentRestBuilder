// <copyright file="IRestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.CollectionTransformation
{
    using System.Collections.Generic;
    using SourcePipes.EntityCollection;
    using Transformers.Hal;

    public interface IRestCollectionLinkGenerator
    {
        IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo);
    }
}
