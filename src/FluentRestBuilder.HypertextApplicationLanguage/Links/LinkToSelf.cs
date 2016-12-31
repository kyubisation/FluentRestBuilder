// <copyright file="LinkToSelf.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    public class LinkToSelf : NamedLink
    {
        public LinkToSelf(Link link)
            : base(Link.Self, link)
        {
        }
    }
}
