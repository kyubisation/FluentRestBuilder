// <copyright file="IRestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Pipes.CollectionMapping
{
    using System.Collections.Generic;
    using Links;
    using Operators.ClientRequest;

    public interface IRestCollectionLinkGenerator
    {
        IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo);
    }
}
