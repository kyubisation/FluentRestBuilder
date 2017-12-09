// <copyright file="LinkExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System.Collections.Generic;
    using System.Linq;

    public static class LinkExtension
    {
        public static IDictionary<string, object> BuildLinks(this IEnumerable<NamedLink> links) =>
            links?.GroupBy(l => l.Name)
                .ToDictionary(g => g.Key, ToSingleOrList);

        private static object ToSingleOrList(IEnumerable<NamedLink> links)
        {
            var linkList = links.ToList();
            if (linkList.Count == 1 && !linkList.First().IsLinkList)
            {
                return linkList.First().Link;
            }

            return linkList
                .Select(l => l.Link)
                .ToList();
        }
    }
}
