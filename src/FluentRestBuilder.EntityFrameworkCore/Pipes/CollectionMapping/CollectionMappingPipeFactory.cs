// <copyright file="CollectionMappingPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.CollectionMapping
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Pipes.CollectionMapping;
    using Hal;
    using Storage;

    public class CollectionMappingPipeFactory<TInput, TOutput> :
        ICollectionMappingPipeFactory<TInput, TOutput>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;

        public CollectionMappingPipeFactory(
            IRestCollectionLinkGenerator linkGenerator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage)
        {
            this.linkGenerator = linkGenerator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
        }

        public OutputPipe<RestEntityCollection> Resolve(
                Func<TInput, TOutput> transformation, IOutputPipe<IQueryable<TInput>> parent) =>
            new CollectionMappingPipe<TInput, TOutput>(
                transformation, this.linkGenerator, this.paginationMetaInfoStorage, parent);
    }
}