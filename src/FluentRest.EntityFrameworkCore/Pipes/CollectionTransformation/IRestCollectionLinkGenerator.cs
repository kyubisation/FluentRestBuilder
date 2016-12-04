// <copyright file="IRestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.CollectionTransformation
{
    using System.Collections.Generic;
    using Core.Common;
    using Core.Transformers.Hal;

    public interface IRestCollectionLinkGenerator
    {
        IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo);
    }
}
