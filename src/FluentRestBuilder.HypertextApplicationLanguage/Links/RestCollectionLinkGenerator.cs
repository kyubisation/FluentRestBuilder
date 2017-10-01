// <copyright file="RestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Links
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Primitives;
    using Operators.ClientRequest;

    public class RestCollectionLinkGenerator : IRestCollectionLinkGenerator
    {
        private readonly HttpRequest request;

        public RestCollectionLinkGenerator(IScopedStorage<HttpContext> httpContextStorage)
        {
            this.request = httpContextStorage.Value.Request;
        }

        public string LimitQueryArgumentKey { get; set; } = "limit";

        public string OffsetQueryArgumentKey { get; set; } = "offset";

        public IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo)
        {
            yield return new LinkToSelf(new Link(UriHelper.GetDisplayUrl(this.request)));

            if (paginationMetaInfo == null)
            {
                yield break;
            }

            if (paginationMetaInfo.Offset > 0)
            {
                yield return this.CreateFirstPageLink(paginationMetaInfo);
                yield return this.CreatePreviousPageLink(paginationMetaInfo);
            }

            if (paginationMetaInfo.Offset + paginationMetaInfo.Limit < paginationMetaInfo.Total)
            {
                yield return this.CreateNextPageLink(paginationMetaInfo);
                yield return this.CreateLastPageLink(paginationMetaInfo);
            }
        }

        private NamedLink CreateFirstPageLink(PaginationMetaInfo metaInfo) =>
            new NamedLink("first", this.CreatePageLink(0, metaInfo.Limit));

        private NamedLink CreatePreviousPageLink(PaginationMetaInfo metaInfo)
        {
            var previousOffset = metaInfo.Offset - metaInfo.Limit;
            return new NamedLink("previous", this.CreatePageLink(previousOffset, metaInfo.Limit));
        }

        private NamedLink CreateNextPageLink(PaginationMetaInfo metaInfo)
        {
            var nextOffset = metaInfo.Offset + metaInfo.Limit;
            return new NamedLink("next", this.CreatePageLink(nextOffset, metaInfo.Limit));
        }

        private NamedLink CreateLastPageLink(PaginationMetaInfo metaInfo)
        {
            var modulo = metaInfo.Total % metaInfo.Limit;
            var lastOffset = modulo == 0
                ? metaInfo.Total - metaInfo.Limit : metaInfo.Total - modulo;
            return new NamedLink("last", this.CreatePageLink(lastOffset, metaInfo.Limit));
        }

        private string CreatePageLink(int offset, int limit)
        {
            var queryParameters = Enumerable.ToDictionary<KeyValuePair<string, StringValues>, string, string>(this.request.Query, q => q.Key, q => q.Value.ToString(), StringComparer.OrdinalIgnoreCase);
            queryParameters[this.OffsetQueryArgumentKey] = offset.ToString();
            queryParameters[this.LimitQueryArgumentKey] = limit.ToString();

            var hostComponents = this.request.Host.ToUriComponent().Split(':');
            var uriBuilder = new UriBuilder
            {
                Scheme = this.request.Scheme,
                Host = hostComponents[0],
                Path = this.request.Path,
            };

            if (hostComponents.Length == 2)
            {
                uriBuilder.Port = Convert.ToInt32((string)hostComponents[1]);
            }

            return QueryHelpers.AddQueryString(uriBuilder.Uri.AbsoluteUri, queryParameters);
        }
    }
}