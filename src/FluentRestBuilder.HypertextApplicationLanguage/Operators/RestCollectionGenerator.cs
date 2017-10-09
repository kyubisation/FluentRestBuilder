// <copyright file="RestCollectionGenerator.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Operators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentRestBuilder.Operators.ClientRequest;
    using Links;

    public class RestCollectionGenerator<TEntity, TResponse> : IRestCollectionGenerator<TEntity, TResponse>
    {
        private readonly ILinkHelper linkHelper;

        public RestCollectionGenerator(ILinkHelper linkHelper)
        {
            this.linkHelper = linkHelper;
        }

        public string LimitQueryArgumentKey { get; set; } = "limit";

        public string OffsetQueryArgumentKey { get; set; } = "offset";

        protected List<TEntity> Entities { get; private set; }

        protected Func<TEntity, TResponse> EntityMapping { get; private set; }

        protected PaginationInfo Info { get; private set; }

        public IRestEntity CreateCollection(
            IEnumerable<TEntity> entities,
            Func<TEntity, TResponse> mapping,
            PaginationInfo paginationInfo = null)
        {
            this.Entities = entities.ToList();
            this.EntityMapping = mapping;
            this.Info = paginationInfo;
            return this.CreateCollection();
        }

        protected virtual IRestEntity CreateCollection() =>
            new RestEntityCollection
            {
                _embedded = this.GenerateEmbeddedDictionary(),
                _links = this.GenerateLinks().BuildLinks(),
                Limit = this.Info?.Limit,
                Total = this.Info?.Total,
                Offset = this.Info?.Offset,
            };

        protected virtual IDictionary<string, object> GenerateEmbeddedDictionary() =>
            new Dictionary<string, object>
            {
                ["item"] = this.Entities
                    .Select(e => this.EntityMapping(e))
                    .ToList(),
            };

        protected virtual IEnumerable<NamedLink> GenerateLinks()
        {
            yield return new LinkToSelf(new Link(this.linkHelper.CurrentUrl()));
            if (this.Info == null)
            {
                yield break;
            }

            foreach (var paginationLink in this.GeneratePaginationLinks())
            {
                yield return paginationLink;
            }
        }

        private IEnumerable<NamedLink> GeneratePaginationLinks()
        {
            if (this.Info.Offset > 0)
            {
                yield return this.PaginationLink("first", null, this.Info.Limit);
                yield return this.PaginationLink(
                    "previous", this.Info.Offset - this.Info.Limit, this.Info.Limit);
            }

            if (this.Info.Offset + this.Info.Limit < this.Info.Total)
            {
                yield return this.PaginationLink(
                    "next", this.Info.Offset + this.Info.Limit, this.Info.Limit);
                var modulo = this.Info.Total % this.Info.Limit;
                var lastOffset = modulo == 0
                    ? this.Info.Total - this.Info.Limit : this.Info.Total - modulo;
                yield return this.PaginationLink("last", lastOffset, this.Info.Limit);
            }
        }

        private NamedLink PaginationLink(string name, int? offset, int limit) =>
            new NamedLink(name, this.linkHelper.ModifyCurrentUrl(new Dictionary<string, string>
            {
                [this.OffsetQueryArgumentKey] = offset?.ToString(),
                [this.LimitQueryArgumentKey] = limit.ToString(),
            }));
    }
}