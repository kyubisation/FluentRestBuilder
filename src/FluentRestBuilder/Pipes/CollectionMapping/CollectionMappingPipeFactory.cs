// <copyright file="CollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using Common;
    using Hal;
    using Storage;

    public class CollectionMappingPipeFactory<TInput, TOutput> :
        ICollectionMappingPipeFactory<TInput, TOutput>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public CollectionMappingPipeFactory(
            IRestCollectionLinkGenerator linkGenerator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer)
        {
            this.linkGenerator = linkGenerator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<RestEntityCollection> Resolve(
                Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent) =>
            new CollectionMappingPipe<TInput, TOutput>(
                transformation,
                this.linkGenerator,
                this.paginationMetaInfoStorage,
                this.queryableTransformer,
                parent);
    }
}