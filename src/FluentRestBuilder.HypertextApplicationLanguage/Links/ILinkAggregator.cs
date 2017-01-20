// <copyright file="ILinkAggregator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System.Collections.Generic;

    public interface ILinkAggregator
    {
        IDictionary<string, object> BuildLinks(IEnumerable<NamedLink> links);
    }
}
