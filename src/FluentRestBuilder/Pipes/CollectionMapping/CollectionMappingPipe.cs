﻿// <copyright file="CollectionMappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.CollectionMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Hal;
    using Pipes;
    using Storage;

    public class CollectionMappingPipe<TInput, TOutput>
        : BaseMappingPipe<IQueryable<TInput>, RestEntityCollection>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly Func<TInput, TOutput> transformation;
        private RestEntityCollection restEntityCollection;

        public CollectionMappingPipe(
            Func<TInput, TOutput> transformation,
            IRestCollectionLinkGenerator linkGenerator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.transformation = transformation;
            this.linkGenerator = linkGenerator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        protected override async Task<RestEntityCollection> MapAsync(IQueryable<TInput> input)
        {
            var entities = await this.queryableTransformer.ToList(input);

            this.restEntityCollection = new RestEntityCollection();
            this.GenerateEmbeddedEntities(entities);
            this.GenerateLinks();

            return this.restEntityCollection;
        }

        private void GenerateEmbeddedEntities(IEnumerable<TInput> entities)
        {
            var transformedEntities = entities
                .Select(e => this.transformation(e))
                .ToList();
            this.restEntityCollection.Embedded.Add("items", transformedEntities);
        }

        private void GenerateLinks()
        {
            var links = this.linkGenerator.GenerateLinks(this.paginationMetaInfoStorage.Value);
            this.restEntityCollection.Links = NamedLink.BuildLinks(links);
        }
    }
}
