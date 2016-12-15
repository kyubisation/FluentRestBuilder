// <copyright file="CollectionMappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.CollectionMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Common;
    using FluentRestBuilder.Pipes.CollectionMapping;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class CollectionMappingPipe<TInput, TOutput>
        : FluentRestBuilder.Pipes.CollectionMapping.CollectionMappingPipe<TInput, TOutput>
    {
        public CollectionMappingPipe(
            Func<TInput, TOutput> transformation,
            IRestCollectionLinkGenerator linkGenerator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(transformation, linkGenerator, paginationMetaInfoStorage, parent)
        {
        }

        protected override Task<List<TInput>> EntitiesToList(IQueryable<TInput> input) =>
            input.ToListAsync();
    }
}
