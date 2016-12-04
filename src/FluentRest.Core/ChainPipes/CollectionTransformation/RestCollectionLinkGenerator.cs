// <copyright file="RestCollectionLinkGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.CollectionTransformation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.WebUtilities;
    using RestCollectionMutators;
    using SourcePipes.EntityCollection;
    using Transformers.Hal;

    public class RestCollectionLinkGenerator : IRestCollectionLinkGenerator
    {
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly HttpRequest request;

        public RestCollectionLinkGenerator(
            IHttpContextAccessor actionContextAccessor,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.request = actionContextAccessor.HttpContext.Request;
            this.queryArgumentKeys = queryArgumentKeys;
        }

        public IEnumerable<NamedLink> GenerateLinks(PaginationMetaInfo paginationMetaInfo)
        {
            yield return new LinkToSelf(new Link(this.request.GetDisplayUrl()));

            if (paginationMetaInfo == null)
            {
                yield break;
            }

            if (paginationMetaInfo.Page > 1)
            {
                yield return new NamedLink("first", new Link(this.CreatePageLink(1)));
                yield return new NamedLink(
                    "previous", new Link(this.CreatePageLink(paginationMetaInfo.Page - 1)));
            }

            if (paginationMetaInfo.Page < paginationMetaInfo.TotalPages)
            {
                yield return new NamedLink(
                    "next", new Link(this.CreatePageLink(paginationMetaInfo.Page + 1)));
                yield return new NamedLink(
                    "last", new Link(this.CreatePageLink(paginationMetaInfo.TotalPages)));
            }
        }

        private string CreatePageLink(int page)
        {
            var queryParameters = this.request.Query
                .ToDictionary(q => q.Key, q => q.Value.ToString());
            queryParameters[this.queryArgumentKeys.Page] = page.ToString();

            var hostComponents = this.request.Host.ToUriComponent().Split(':');
            var uriBuilder = new UriBuilder
            {
                Scheme = this.request.Scheme,
                Host = hostComponents[0],
                Path = this.request.Path
            };

            if (hostComponents.Length == 2)
            {
                uriBuilder.Port = Convert.ToInt32(hostComponents[1]);
            }

            return QueryHelpers.AddQueryString(uriBuilder.Uri.AbsoluteUri, queryParameters);
        }
    }
}