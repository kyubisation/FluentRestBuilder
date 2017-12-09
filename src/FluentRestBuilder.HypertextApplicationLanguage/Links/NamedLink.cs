// <copyright file="NamedLink.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    public class NamedLink
    {
        public NamedLink(string name, Link link)
        {
            this.Name = name;
            this.Link = link;
        }

        public NamedLink(string name, string url)
            : this(name, new Link(url))
        {
        }

        public string Name { get; }

        public Link Link { get; }

        public bool IsLinkList { get; set; }
    }
}
